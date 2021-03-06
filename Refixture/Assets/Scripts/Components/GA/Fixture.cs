﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// MonoBehavior script that contains the information of the fixture and some states. Handles repositioning the game object and checks if it's stuck.
/// </summary>
public class Fixture : MonoBehaviour
{
    [SerializeField] private FixtureSO _fixtureSO;
    public RoomGA roomGA;
    

    private Rigidbody _thisRigidbody;
    private PosConstraints _thisConstraints;
    [SerializeField] private float _localFitness;
    [SerializeField] private bool _isTouchingWall;
    private bool _isMoving;
    private bool _isPushed;
    private bool _notMoved;
    private float _timeMoving;
    private float _moveTimeTolerance = 0.6f;
    [SerializeField] private int _collisionCount = 0;
    [SerializeField] private int _triggerCount = 0;

    private bool _valueUpdated;
    private bool _fitnessCalculated;



    public float LocalFitness
    {
        get
        {
            return _localFitness;
        }
        set => _localFitness = value;
    }

    public FixtureSO FixtureSO { get => _fixtureSO; set => _fixtureSO = value; }
    public Rigidbody Rigidbody { get => _thisRigidbody; set => _thisRigidbody = value; }
    public PosConstraints Constraints { get => _thisConstraints; set => _thisConstraints = value; }
    public int CollisionCount
    {
        get => _collisionCount; set
        {
            _collisionCount = value;
            _valueUpdated = true;
        }
    }

    public int TriggerCount
    {
        get => _triggerCount; set
        {
            _triggerCount = value;
            _valueUpdated = true;
        }
    }

    public bool IsTouchingWall
    {
        get => _isTouchingWall; set
        {
            _isTouchingWall = value;
            _valueUpdated = true;
        }
    }

    private void Awake()
    {
        IsTouchingWall = false;
        _isMoving = false;
        _isPushed = false;
        _notMoved = false;
        _valueUpdated = false;
        _fitnessCalculated = false;
        _timeMoving = 0;
    }

    private void FixedUpdate()
    {
        CheckIfStuck();
        
        if (!_isMoving)
        {
            if (Constraints != null)
            {
                CheckIfNearEdge();
            }
            if (_notMoved == true && _valueUpdated)
            {
                CalculateLocalFitness();
                _valueUpdated = false;
            }
            _notMoved = true;
        }
        else
        {
            _notMoved = false;
        }
    }

    private void CheckIfStuck()
    {
        if (_thisRigidbody != null)
        {
            Vector3 horizontalVelocity = _thisRigidbody.velocity;
            horizontalVelocity.y = 0f;
            if (horizontalVelocity.magnitude > 0.025f || _thisRigidbody.angularVelocity.magnitude > 0.015f)
            {
                _isMoving = true;
            }
            else
                _isMoving = false;
        }

        if (_isMoving && !_isPushed)
        {
            _timeMoving += Time.deltaTime;
        }
        else
        {
            _timeMoving = 0;
            _isPushed = false;
        }

        if (_timeMoving > _moveTimeTolerance)
        {
            SetNewRandomPos();
            _timeMoving = 0;
        }
        if (_fixtureSO.IsGroundFixture && transform.localPosition.y > 0.8f)
        {
            SetNewRandomPos();
        }
        if (transform.localPosition.y < -0.05f)
        {
            SetNewRandomPos();
        }
    }

    private void CheckIfNearEdge()
    {
        Vector3 pos = transform.localPosition;
        if (pos.x > Constraints.XMax - 0.1f ||
            pos.x < Constraints.XMin + 0.1f ||
            pos.z > Constraints.ZMax - 0.1f ||
            pos.z < Constraints.ZMin + 0.1f)
        {
            IsTouchingWall = true;
        }
    }

    private void CalculateLocalFitness()
    {
        float value = 0;
        value += EvaluateTriggerCt();
        value += EvaluateCollisionCt();
        //value += EvaluatePosition();
        value += EvaluateClosest();
        value += EvaluateTypes();
        if (value < 0)
            value = 0;
        _localFitness = value;
        //_fitnessCalculated = true;
    }

    public void CalculateLocalFitness(System.Action callback)
    {
        StartCoroutine(FitnessCalculated(callback));
    }

    IEnumerator FitnessCalculated(System.Action callback)
    {
        float maxWaitTime = 5f;
        float waitTime = 0;
        while(_fitnessCalculated && !_isMoving && !_valueUpdated || waitTime >= maxWaitTime)
        {
            yield return new WaitForSeconds(0.1f);
            waitTime += Time.deltaTime;
        }
        _fitnessCalculated = false;
        callback();
    }

    private float EvaluateTypes()
    {
        float value = 0;

        if(_fixtureSO.IsGroundFixture)
        {
            if(transform.localPosition.y > 0.3)
            {
                value -= 100;
            }
        }
        else
        {
            if(transform.localPosition.y > 0.3)
            {
                value += 20;
            }
        }
        if(_fixtureSO.IsWallFixture)
        {
            if(!_isTouchingWall)
            {
                value -= 100;
            }
        }
        else if (_isTouchingWall)
        {
            value += 10;
        }

        return value;
    }

    private float EvaluateTriggerCt()
    {
        float value = 0;
        switch(TriggerCount)
        {
            case 0:
                value = 20;
                break;
            case 1:
                value = -6;
                break;
            case 2:
                value = -9;
                break;
            case 3:
                value = -20;
                break;
            default:
                value = -20;
                break;
        }

        return value;
    }

    private float EvaluateCollisionCt()
    {
        float value = 0;
        switch (CollisionCount)
        {
            case 0:
                value = 8;
                break;
            case 1:
                value = 6;
                break;
            case 2:
                value = 4;
                break;
            case 3:
                value = 2;
                break;
            case 4:
                value = 1;
                break;
            default:
                value = 0;
                break;
        }

        return value;
    }

    private float EvaluatePosition()
    {
        float value = 0;

        value += Mathf.Abs(transform.localPosition.sqrMagnitude);

        return value;
    }

    private float EvaluateClosest()
    {
        float value = 0;
        var transforms = from gameObject in roomGA.FixtureObjects
                         select gameObject.transform;
        var closest = ReturnClosest(transforms.ToArray());
        var closestScript = closest.GetComponent<Fixture>();
        if (!_fixtureSO.IsGroundFixture)
        {
            if (closestScript.FixtureSO.IsOfType("Surface"))
            {
                value += 15;
            }
        }
        

        return value;
    }

    private float ReturnClosestDistance(Transform[] transforms)
    {
        var closest = ReturnClosest(transforms);
        return (closest.position - transform.position).sqrMagnitude;
    }
    private Transform ReturnClosest(Transform[] transforms)
    {
        return transforms.OrderBy(t => (t.position - transform.position).sqrMagnitude)
                                .ElementAt(1);
    }

    private void OnCollisionEnter(Collision collision)
    {
        CollisionCount++;
        ContactPoint[] contactPoints = new ContactPoint[collision.contactCount];
        collision.GetContacts(contactPoints);
        TestContactPoints(contactPoints);
    }

    private void OnCollisionExit()
    {
        CollisionCount--;
    }

    private void OnTriggerEnter()
    {
        TriggerCount++;
    }

    private void OnTriggerExit()
    {
        TriggerCount--;
    }

    private void TestContactPoints(ContactPoint[] contactPoints)
    {
        IsTouchingWall = false;
        foreach (ContactPoint point in contactPoints)
        {

            Fixture otherFixture;
            point.otherCollider.gameObject.TryGetComponent<Fixture>(out otherFixture);
            if (otherFixture != null)
            {
                TestOtherFixture(otherFixture);
            }
        }
    }

    private void TestOtherFixture(Fixture otherFixture)
    {
        if (otherFixture.FixtureSO.IsOfType("Wall"))
        {
            IsTouchingWall = true;
        }
    }

    public void SetNewRandomPos()
    {
        IsTouchingWall = false;
        Vector3 pos = transform.localPosition;
        pos.x = Random.Range(Constraints.XMin, Constraints.XMax);
        pos.y = 0;
        pos.z = Random.Range(Constraints.ZMin, Constraints.ZMax);
        transform.localPosition = pos;
        _thisRigidbody.velocity = Vector3.zero;
        _thisRigidbody.angularVelocity = Vector3.zero;
    }

    public void SetNewRandomRot()
    {
        Vector3 rot = transform.localEulerAngles;
        int rand = Random.Range(0, 4);
        rot.y = rand * 90f;
        transform.localEulerAngles = rot;
    }

    public void RandomPush()
    {
        Vector3 forceVector = new Vector3(Random.Range(-8f, 8f), 0, Random.Range(-8f, 8f));
        _thisRigidbody.AddForce(forceVector, ForceMode.VelocityChange);
        _isPushed = true;
    }
}
