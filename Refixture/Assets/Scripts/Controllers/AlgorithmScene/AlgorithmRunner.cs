using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AlgorithmRunner : MonoBehaviour
{
    [SerializeField] private SettingsSO _settingsSO;
    [SerializeField] private FloatEventChannelSO _bestFitnessCh;
    private List<FixtureContainerSO> _matingPool;
    private List<float> _bestFitnesses;
    private List<FixtureContainerSO> _best;
    private int _maxBest = 15;

    public List<FixtureContainerSO> Best { get => _best; set => _best = value; }

    public void StartNewGeneration(List<RoomGA> pop, System.Action generationDone)
    {
        _matingPool = GenerateMatingPool(pop);
        GenerateOffspring(pop);
        generationDone();
    }

    private List<FixtureContainerSO> GenerateMatingPool(List<RoomGA> pop)
    {
        NormalizeFitness(pop);

        List<FixtureContainerSO> parents = RouletteWheelSelection(pop, GenerateRandomPoints(pop.Count));
        return parents;
    }

    private void GenerateOffspring(List<RoomGA> pop)
    {
        for (int i = 0; i < pop.Count; i++)
        {
            if (Random.value <= _settingsSO.CrossoverRate)
            {
                int j = i + 1;
                if (j == pop.Count) j = 0;
                pop[i].Crossover(_matingPool[i], _matingPool[j]);
            }
        }

        for (int i = 0; i < pop.Count; i++)
        {
            if (Random.value <= _settingsSO.MutationRate)
            {
                pop[i].Mutate(_settingsSO.MutationRate);
            }
        }
    }

    private List<FixtureContainerSO> RouletteWheelSelection(List<RoomGA> pop, List<float> points)
    {
        int popCount = pop.Count();

        var sortedRooms = pop.OrderBy(room => room.Fitness);

        SetNewBest(sortedRooms.Last());

        List<FixtureContainerSO> selections = new List<FixtureContainerSO>();

        foreach (float point in points)
        {
            float sum = 0;
            foreach (RoomGA room in sortedRooms)
            {
                sum += room.normalizedFitness;
                if (sum >= point)
                {
                    selections.Add(Instantiate(room.Fixtures));
                    break;
                }
            }
        }


        return selections;
    }

    private List<float> GenerateRandomPoints(int count)
    {
        var listOfPoints = new List<float>();

        for (int i = 0; i < count; i++)
        {
            listOfPoints.Add(Random.value);
        }

        return listOfPoints;
    }

    private List<float> GenerateStochasticPoints(int count)
    {
        float step = 1 / count;
        var listOfPoints = new List<float>();
        float start = Random.Range(0, step);

        for(int i = 0; i < count; i++)
        {
            listOfPoints.Add(start + i * step);
        }

        return listOfPoints;
    }

    private void NormalizeFitness(List<RoomGA> pop)
    {
        float totalFitness = pop.Sum(room => room.Fitness);
        foreach (var room in pop)
        {
            room.normalizedFitness = room.Fitness / totalFitness;
        }
    }

    private void SetNewBest(RoomGA candidate)
    {
        float fitness = candidate.Fitness;
        if (_best == null)
        {
            _best = new List<FixtureContainerSO>();
            _bestFitnesses = new List<float>();
        }
        if (_best.Count == 0 || fitness > _bestFitnesses.First())
        {
            FixtureContainerSO newBest = Instantiate(candidate.Fixtures);
            _best.Add(newBest);
            _bestFitnesses.Add(fitness);

            if (_best.Count > _maxBest)
            {
                _best.RemoveAt(0);
                _bestFitnesses.RemoveAt(0);
            }
            _bestFitnessCh.RaiseEvent(_bestFitnesses.Max());
        }
    }
}
