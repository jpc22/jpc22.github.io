using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MonoBehavior script that contains the information of the fixture and some states. Handles repositioning the game object and checks if it's stuck.
/// </summary>
public class Fixture : MonoBehaviour
{
    [SerializeField] private FixtureSO _fixtureSO;
    

    private Rigidbody _thisRigidbody;
    private PosConstraints _thisConstraints;
    [SerializeField] private float _localFitness;
    [SerializeField] private bool _isTouchingWall;
    private bool _isMoving;
    private float _timeMoving;
    private float _moveTimeTolerance = 0.2f;

    public float LocalFitness
    {
        get
        {
            CalculateLocalFitness();
            return _localFitness;
        }
        set => _localFitness = value;
    }

    public FixtureSO FixtureSO { get => _fixtureSO; set => _fixtureSO = value; }
    public Rigidbody Rigidbody { get => _thisRigidbody; set => _thisRigidbody = value; }
    public PosConstraints Constraints { get => _thisConstraints; set => _thisConstraints = value; }

    private void Awake()
    {
        _isTouchingWall = false;
        _isMoving = false;
        _timeMoving = 0;
    }

    private void FixedUpdate()
    {
        CheckIfStuck();
        if (!_isMoving && Constraints != null)
        {
            CheckIfNearEdge();
        }
    }

    private void CheckIfStuck()
    {
        if (_thisRigidbody != null)
        {
            if (_thisRigidbody.velocity.magnitude > 0.05f || _thisRigidbody.angularVelocity.magnitude > 0.05f)
            {
                _isMoving = true;
            }
            else
                _isMoving = false;
        }

        if (_isMoving)
        {
            _timeMoving += Time.deltaTime;
        }
        else _timeMoving = 0;

        if (_timeMoving > _moveTimeTolerance)
        {
            SetNewRandomPos();
            _timeMoving = 0;
        }
    }

    private void CheckIfNearEdge()
    {
        Vector3 pos = transform.localPosition;
        if (pos.x > Constraints.XMax - 0.3f ||
            pos.x < Constraints.XMin + 0.3f ||
            pos.z > Constraints.ZMax - 0.3f ||
            pos.z < Constraints.ZMin + 0.3f)
        {
            _isTouchingWall = true;
        }
    }

    private void CalculateLocalFitness()
    {
        float value = 0;
        if (_isTouchingWall) value += 1;
        _localFitness = value;
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint[] contactPoints = new ContactPoint[collision.contactCount];
        collision.GetContacts(contactPoints);
        TestContactPoints(contactPoints);
    }

    private void TestContactPoints(ContactPoint[] contactPoints)
    {
        _isTouchingWall = false;
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
            _isTouchingWall = true;
        }
    }

    public void SetNewRandomPos()
    {
        _isTouchingWall = false;
        Vector3 pos = transform.localPosition;
        pos.x = Random.Range(Constraints.XMin, Constraints.XMax);
        pos.z = Random.Range(Constraints.ZMin, Constraints.ZMax);
        transform.localPosition = pos;
        _thisRigidbody.velocity = Vector3.zero;
        _thisRigidbody.angularVelocity = Vector3.zero;
    }

    public void SetNewRandomRot()
    {
        Vector3 rot = transform.localEulerAngles;
        rot.y = Random.Range(-180, 180);
        transform.localEulerAngles = rot;
    }
}
