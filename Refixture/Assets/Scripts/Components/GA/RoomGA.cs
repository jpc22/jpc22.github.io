using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private float _fitness;

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
            CalculateFitness();
            return _fitness;
        } 
        set => _fitness = value;
    }

    public void InitializeFixtures()
    {
        List<FixtureSO> fixtureList = _fixtures.Fixtures;
        for (int i = 0; i < fixtureList.Count; i++)
        {
            GameObject fixture = Instantiate(fixtureList[i].Prefab3d, transform);
            fixture.name = fixtureList[i].Name;
            fixture.transform.localScale = _fixtures.ScaleAt(i);
            _fixtureObjects.Add(fixture);

            Fixture fixtureScript = fixture.AddComponent<Fixture>();
            fixtureScript.FixtureSO = fixtureList[i];
            _fixtureScripts.Add(fixtureScript);

            PosConstraints posC = fixture.AddComponent<PosConstraints>();
            posC.SetLocalConstraints(-_dimensions.x / 2, _dimensions.x / 2, 0f, _dimensions.y, -_dimensions.z / 2, _dimensions.z / 2);
            posC.SetConstraintsOnBounds();
            fixtureScript.Constraints = posC;

            Rigidbody rb = fixture.AddComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            _fixtureRBs.Add(rb);
            fixtureScript.Rigidbody = rb;

            RandomizePosition(i);
            RandomizeRotation(i);
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

    public void RandomizeRotation(int index)
    {
        _fixtureScripts[index].SetNewRandomRot();
    }

    public void CalculateFitness()
    {

    }

    private void RecordFixtures()
    {
        for (int i = 0; i < _fixtureObjects.Count; i++)
        {
            Transform objT = _fixtureObjects[i].transform;
            _fixtures.SetPosAt(i, objT.localPosition.x, objT.localPosition.y, objT.localPosition.z);
            _fixtures.SetRotAt(i, objT.localEulerAngles.x, objT.localEulerAngles.y, objT.localEulerAngles.z);
        }
    }
}
