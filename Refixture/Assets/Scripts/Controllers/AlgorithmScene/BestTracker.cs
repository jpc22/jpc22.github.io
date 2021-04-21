using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BestTracker : MonoBehaviour
{
    List<Solution> _solutionSet;
    List<Solution> _fitnessSet;
    List<Solution> _noveltySet;
    static int fitMaxCount = 15;
    static int novelMaxCount = 15;
    static int solutionMaxCount = 15;

    public List<Solution> SolutionSet
    {
        get
        {
            BuildSolutionSet();
            return _solutionSet;
        }
        set
        {
            _solutionSet = value;
        }
    }
    public List<Solution> NoveltySet { get => _noveltySet; set => _noveltySet = value; }
    public List<Solution> FitnessSet { get => _fitnessSet; set => _fitnessSet = value; }

    private void Awake()
    {
        _solutionSet = new List<Solution>();
        _fitnessSet = new List<Solution>();
        _noveltySet = new List<Solution>();
    }

    public void TestNewBest(RoomGA candidate)
    {
        Solution solution = new Solution(Instantiate(candidate.Fixtures), candidate.Fitness, new Vector3(candidate.NoveltyVector.x,
                                                                                                         candidate.NoveltyVector.y,
                                                                                                         candidate.NoveltyVector.z));
        TryAddToFitList(solution);

        TryAddToNovelList(solution);
    }

    private void TryAddToFitList(Solution solution)
    {
        if (_fitnessSet.Count == 0)
            _fitnessSet.Add(solution);
        else if (_fitnessSet.Count < fitMaxCount || _fitnessSet.Last().fitness < solution.fitness)
        {
            _fitnessSet.Add(solution);
            _fitnessSet = _fitnessSet.OrderByDescending(c => c.fitness).ToList();
            if (_fitnessSet.Count > fitMaxCount)
            {
                _fitnessSet.RemoveAt(fitMaxCount);
            }
        }
    }

    private void TryAddToNovelList(Solution solution)
    {
        if (_noveltySet.Count == 0)
            _noveltySet.Add(solution);
        else if (_noveltySet.Count < novelMaxCount || IsNovel(solution))
        {
            _noveltySet.Add(solution);
            _noveltySet = _noveltySet.OrderByDescending(c => c.noveltyDiff).ToList();
            if (_noveltySet.Count > novelMaxCount)
            {
                _noveltySet.RemoveAt(novelMaxCount);
            }
        }
    }

    private bool IsNovel(Solution candidate)
    {
        float avg = _noveltySet.Average(c => c.noveltyMagnitude);

        candidate.noveltyDiff = Mathf.Abs(avg - candidate.noveltyMagnitude);
        
        if (candidate.noveltyDiff > _noveltySet.Average(c => c.noveltyDiff))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void BuildSolutionSet()
    {
        int i = 0;
        while (true)
        {
            _solutionSet.Add(_fitnessSet[i]);
            if (_solutionSet.Count == solutionMaxCount) break;
            _solutionSet.Add(_noveltySet[i]);
            if (_solutionSet.Count == solutionMaxCount) break;

            i++;
        }
    }
}

public class Solution
{
    public FixtureContainerSO containerSO;
    public float fitness;
    public Vector3 noveltyVector;
    public float noveltyMagnitude;
    public float noveltyDiff;
    public Solution(FixtureContainerSO container, float fit, Vector3 novelty)
    {
        containerSO = container;
        fitness = fit;
        noveltyVector = novelty;
        noveltyMagnitude = novelty.sqrMagnitude;
        noveltyDiff = 0;
    }
}