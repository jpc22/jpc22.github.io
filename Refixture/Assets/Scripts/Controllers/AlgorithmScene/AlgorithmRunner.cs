using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AlgorithmRunner : MonoBehaviour
{
    [SerializeField] private SettingsSO _settingsSO;
    [SerializeField] private FloatEventChannelSO _bestFitnessCh;
    private List<FixtureContainerSO> _matingPool;
    private List<FixtureContainerSO> _best;
    private BestTracker _bestTracker;

    public List<FixtureContainerSO> Best
    {
        get
        {
            _best = _bestTracker.SolutionSet.Select(c => c.containerSO).ToList();
            return _best;
        }
        set
        {
            _best = value;
        }
    }

    private void Awake()
    {
        _bestTracker = gameObject.AddComponent<BestTracker>();
    }

    public void StartNewGeneration(List<RoomGA> pop, System.Action generationDone)
    {
        _bestTracker.TestNewBest(pop.OrderByDescending(c => c.Fitness).First());
        _bestFitnessCh.RaiseEvent(_bestTracker.FitnessSet.First().fitness);
        _matingPool = GenerateMatingPool(pop);
        GenerateOffspring(pop);
        generationDone();
    }

    private List<FixtureContainerSO> GenerateMatingPool(List<RoomGA> pop)
    {
        //NormalizeFitness(pop);

        //List<FixtureContainerSO> parents = RouletteWheelSelection(pop, GenerateRandomPoints(pop.Count));
        List<FixtureContainerSO> parents = TournamentSelection(pop.Count / 4, pop.Count / 8, pop);

        parents.Add(Instantiate(_bestTracker.FitnessSet.First().containerSO));
        parents.AddRange(_bestTracker.NoveltySet.Select(c => Instantiate(c.containerSO)));
        return parents;
    }

    private void GenerateOffspring(List<RoomGA> pop)
    {
        for (int i = 0; i < pop.Count; i++)
        {
            if (Random.value <= _settingsSO.CrossoverRate)
            {
                int rand1 = Random.Range(0, _matingPool.Count);
                int rand2;
                do
                {
                    rand2 = Random.Range(0, _matingPool.Count);
                } while (rand2 == rand1);

                pop[i].Crossover(_matingPool[rand1], _matingPool[rand2]);
            }
            else
            {
                pop[i].Copy(_matingPool[i % _matingPool.Count]);
            }
        }

        for (int i = 0; i < pop.Count; i++)
        {
            //if (Random.value <= _settingsSO.MutationRate)
            //{
                pop[i].Mutate(_settingsSO.MutationRate);
            //}
        }
    }

    private List<FixtureContainerSO> RouletteWheelSelection(List<RoomGA> pop, List<float> points)
    {
        int popCount = pop.Count();

        var sortedRooms = pop.OrderBy(room => room.Fitness);

        Debug.Log("Worst of generation Fitness: " + sortedRooms.First().Fitness);
        Debug.Log("Best of generation fitness: " + sortedRooms.Last().Fitness);

        _bestTracker.TestNewBest(sortedRooms.Last());

        List<FixtureContainerSO> selections = new List<FixtureContainerSO>();
        var selectedRooms = new List<RoomGA>();

        foreach (float point in points)
        {
            float sum = 0;
            foreach (RoomGA room in sortedRooms)
            {
                sum += room.normalizedFitness;
                if (sum >= point)
                {
                    selections.Add(Instantiate(room.Fixtures));
                    selectedRooms.Add(room);
                    break;
                }
            }
        }

        var sortedSelections = selectedRooms.OrderBy(room => room.Fitness);
        Debug.Log("Worst of selections: " + sortedSelections.First().Fitness);
        Debug.Log("Best of selections: " + sortedSelections.Last().Fitness);


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


    private int[] GetUniqueIndexes(int length, int min, int max)
    {
        var diff = max - min;

        var orderedValues = Enumerable.Range(min, diff).ToList();
        var ints = new int[length];

        for (int i = 0; i < length; i++)
        {
            var removeIndex = Random.Range(0, orderedValues.Count);
            ints[i] = orderedValues[removeIndex];
            orderedValues.RemoveAt(removeIndex);
        }

        return ints;
    }

    private List<FixtureContainerSO> TournamentSelection(int tournamentSize, int poolSize, List<RoomGA> pop)
    {

        var selected = new List<FixtureContainerSO>();
        var selectedRooms = new List<RoomGA>();


        while (selected.Count < poolSize)
        {
            var randomIndexes = GetUniqueIndexes(tournamentSize, 0, pop.Count);
            var tournamentWinner = pop.Where((c, i) => randomIndexes.Contains(i)).OrderByDescending(c => c.Fitness).First();

            //Debug.Log("Tournament Winner: " + tournamentWinner.Fitness);

            selected.Add(tournamentWinner.Fixtures);
            selectedRooms.Add(tournamentWinner);

            bool winnerContinues = true;
            if (!winnerContinues)
            {
                pop.Remove(tournamentWinner);
            }
        }
        
        //var sortedRooms = selectedRooms.OrderByDescending(c => c.Fitness);
        //Debug.Log("Best of tournaments: " + sortedRooms.First().Fitness);
        //Debug.Log("Worst of tournaments: " + sortedRooms.Last().Fitness);

        return selected;
    }
}


