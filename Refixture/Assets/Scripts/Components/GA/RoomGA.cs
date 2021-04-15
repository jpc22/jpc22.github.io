using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RefixUtilities;

/// <summary>
/// One of a population of rooms used in the GA
/// </summary>
public class RoomGA : MonoBehaviour
{
    private FixtureContainerSO _fixtures;
    private FixtureContainerSO _originalFixtures;
    private List<GameObject> _fixtureObjects = new List<GameObject>();
    private List<Fixture> _fixtureScripts = new List<Fixture>();
    private List<Rigidbody> _fixtureRBs = new List<Rigidbody>();
    private int _roomIndex;
    private Vector3 _dimensions; // x = length, y = height, z = width

    [SerializeField] private float _fitness;
    public float normalizedFitness;

    public FixtureContainerSO Fixtures
    {
        get
        {
            RecordFixtures();
            return _fixtures;
        }
        set
        {
            _originalFixtures = value;
            _fixtures = Instantiate(value);
            _fixtures.UpdateChannel = null;
        }
    }

    public int Index { get => _roomIndex; set => _roomIndex = value; }
    public Vector3 Dimensions { get => _dimensions; set => _dimensions = value; }
    public float Fitness
    {
        get
        {
            //CalculateFitness();
            return _fitness;
        } 
        set => _fitness = value;
    }

    public List<GameObject> FixtureObjects { get => _fixtureObjects; set => _fixtureObjects = value; }

    public void InitializeFixtures()
    {
        List<FixtureSO> fixtureList = _fixtures.Fixtures;
        for (int i = 0; i < fixtureList.Count; i++)
        {
            GameObject fixture = Instantiate(fixtureList[i].GetPrefab3d(), transform);
            fixture.name = fixtureList[i].Name;
            fixture.transform.localScale = _fixtures.ScaleAt(i);
            _fixtureObjects.Add(fixture);

            Fixture fixtureScript = fixture.AddComponent<Fixture>();
            fixtureScript.FixtureSO = fixtureList[i];
            fixtureScript.roomGA = this;
            _fixtureScripts.Add(fixtureScript);

            PosConstraints posC = fixture.AddComponent<PosConstraints>();
            posC.SetLocalConstraints(-_dimensions.x / 2, _dimensions.x / 2, 0f, _dimensions.y, -_dimensions.z / 2, _dimensions.z / 2);
            fixtureScript.Constraints = posC;

            Rigidbody rb = fixture.AddComponent<Rigidbody>();
            if(fixtureScript.FixtureSO.IsGravityEnabled)
            {
                rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            }
            else
            {
                rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            }
            _fixtureRBs.Add(rb);
            fixtureScript.Rigidbody = rb;

            RandomizePosition(i);
            RandomizeRotation(i);
            RandomPush(i);
        }
    }

    public void ReInitializeFixtures()
    {
        for (int i = 0; i < _fixtureObjects.Count; i++)
        {
            _fixtureObjects[i].transform.localPosition = _fixtures.PosAt(i);
            _fixtureObjects[i].transform.localEulerAngles = _fixtures.RotAt(i);
        }
    }

    public void RandomizePositions()
    {
        for(int i = 0; i < _fixtureObjects.Count; i++) RandomizePosition(i);
    }

    public void RandomizeRotations()
    {
        for (int i = 0; i < _fixtureObjects.Count; i++) RandomizeRotation(i);
    }

    public void RandomizePosition(int index)
    {
        _fixtureScripts[index].SetNewRandomPos();
    }

    public void RandomPush(int index)
    {
        _fixtureScripts[index].RandomPush();
    }

    public void RandomizeRotation(int index)
    {
        _fixtureScripts[index].SetNewRandomRot();
    }

    public void CalculateFitness()
    {
        float value = 0;
        float sumOfFixtures = 0;
        foreach (Fixture fixture in _fixtureScripts)
        {
            sumOfFixtures += fixture.LocalFitness;
        }

        value += sumOfFixtures;

        _fitness = value;
    }

    public void CalculateFitness(System.Action callback)
    {
        CallbackCounter cbct = new CallbackCounter(_fixtureScripts.Count, () => 
        { 
            CalculateFitness();
            //Debug.Log("All local fixtures calculated. Fitness = " + _fitness);
            callback();
        });
        foreach (Fixture fixture in _fixtureScripts)
        {
            fixture.CalculateLocalFitness(cbct.Callback);
        }
        
    }

    public void Crossover(FixtureContainerSO parentA, FixtureContainerSO parentB)
    {
        UniformCrossover(parentA, parentB);
        ReInitializeFixtures();
    }

    private void UniformCrossover(FixtureContainerSO parentA, FixtureContainerSO parentB)
    {
        for (int i = 0; i < _fixtureScripts.Count; i++)
        {
            if (Random.value >= 0.5f)
            {
                _fixtures.Copy(parentA, i);
            }
            else
            {
                _fixtures.Copy(parentB, i);
            }
        }
    }

    public void Mutate(float mutateRate)
    {
        foreach (Fixture fixture in _fixtureScripts)
        {
            if (Random.value <= mutateRate)
            {
                if(Random.value <= mutateRate / 2)
                {
                    fixture.SetNewRandomPos();
                }
                fixture.RandomPush();
            }
            else if (Random.value <= mutateRate)
            {
                fixture.SetNewRandomRot();
            }
        }
    }

    private void RecordFixtures()
    {
        for (int i = 0; i < _fixtureObjects.Count; i++)
        {
            Transform objT = _fixtureObjects[i].transform;
            _fixtures.SetScaleAt(i, objT.localScale.x, objT.localScale.y, objT.localScale.z);
            _fixtures.SetPosAt(i, objT.localPosition.x, objT.localPosition.y, objT.localPosition.z);
            _fixtures.SetRotAt(i, objT.localEulerAngles.x, objT.localEulerAngles.y, objT.localEulerAngles.z);
        }
    }
}
