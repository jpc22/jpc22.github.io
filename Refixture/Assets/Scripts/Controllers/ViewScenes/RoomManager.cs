using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private FixtureContainerSO _roomObjects;
    [SerializeField] private BoolEventChannelSO _moveChannel;
    [SerializeField] private BoolEventChannelSO _rotateChannel;
    [SerializeField] private BoolEventChannelSO _elevateChannel;
    [SerializeField] private SettingsSO _settingsSO;
    [SerializeField] private SaveManager _saveManager;
    
    private GameObject _rootObject;
    private List<GameObject> _objectList;
    private bool _moveState;
    private bool _rotateState;
    private bool _elevateState;


    private void Awake()
    {
        _rootObject = new GameObject("Room");
        InitializeFixtures();
    }

    void Start()
    {
        
    }

    private void OnEnable()
    {
        _roomObjects.RemovedChannel.OnEventRaised += OnObjectRemoved;
        _roomObjects.AddedChannel.OnEventRaised += OnObjectAdded;
        _roomObjects.UpdateChannel.OnEventRaised += OnContainerUpdate;
        _moveChannel.OnEventRaised += OnMoveToggle;
        _rotateChannel.OnEventRaised += OnRotateToggle;
        _elevateChannel.OnEventRaised += OnElevateToggle;
    }
    private void OnDisable()
    {
        _roomObjects.RemovedChannel.OnEventRaised -= OnObjectRemoved;
        _roomObjects.AddedChannel.OnEventRaised -= OnObjectAdded;
        _roomObjects.UpdateChannel.OnEventRaised -= OnContainerUpdate;
        _moveChannel.OnEventRaised -= OnMoveToggle;
        _rotateChannel.OnEventRaised -= OnRotateToggle;
        _elevateChannel.OnEventRaised += OnElevateToggle;
    }
    private void InitializeFixtures()
    {
        for (int i = 0; i < _roomObjects.Fixtures.Count; i++)
        {
            AddToRoot(i);
            UpdateScale(i);
            UpdatePos(i);
            UpdateRot(i);
        }
    }

    private void AddToRoot(int index)
    {
        if (_objectList == null)
            _objectList = new List<GameObject>();
        _objectList.Add(Instantiate(_roomObjects.Fixtures[index].GetPrefab3d(), _rootObject.transform));
        _objectList[index].transform.localScale = _roomObjects.ScaleAt(index);
        //add components
        if (!_objectList[index].CompareTag("Ground"))
        {
            Rigidbody rb = _objectList[index].AddComponent<Rigidbody>();
            rb.useGravity = false;
            rb.isKinematic = true;
            _objectList[index].AddComponent<SelectGlow>();
        }
        if (_moveState)
        {
            AddMoveComponent(_objectList[index]);
        }
        else if (_rotateState)
        {
            AddRotateComponent(_objectList[index]);
        }
        else if (_elevateState)
        {
            AddElevateComponent(_objectList[index]);
        }
        //add position recorder
    }

    private void RemoveFromRoot(int index)
    {
        Destroy(_objectList[index]);
        _objectList.RemoveAt(index);
    }

    private void UpdateScale(int index)
    {
        _objectList[index].transform.localScale = _roomObjects.ScaleAt(index);
    }

    private void UpdatePos(int index)
    {
        _objectList[index].transform.localPosition = _roomObjects.PosAt(index);
    }

    private void UpdateRot(int index)
    {
        _objectList[index].transform.localEulerAngles = _roomObjects.RotAt(index);
    }

    private void AddMoveComponents()
    {
        foreach (GameObject obj in _objectList)
        {
            if (!obj.CompareTag("Ground"))
            {
                AddMoveComponent(obj);
            }
        }
    }
    private void AddRotateComponents()
    {
        foreach (GameObject obj in _objectList)
        {
            if (!obj.CompareTag("Ground"))
                AddRotateComponent(obj);
        }
    }
    private void AddElevateComponents()
    {
        foreach (GameObject obj in _objectList)
        {
            if (!obj.CompareTag("Ground"))
                AddElevateComponent(obj);
        }
    }
    
    private void AddMoveComponent(GameObject obj)
    {
        if(obj.TryGetComponent(out PlaneDrag move))
        {
            // Already has PlaneDrag
        }
        else
            obj.AddComponent<PlaneDrag>();
    }
    private void AddRotateComponent(GameObject obj)
    {
        if (obj.TryGetComponent(out RotateDrag rotate))
        {
            // Already has RotateDrag
        }
        else
            obj.AddComponent<RotateDrag>();
    }
    private void AddElevateComponent(GameObject obj)
    {
        if (obj.TryGetComponent(out ElevateDrag elevate))
        { }
        else
            obj.AddComponent<ElevateDrag>();
    }
    private void RemoveMoveComponents()
    {
        foreach (GameObject obj in _objectList)
        {
            if (!obj.CompareTag("Ground"))
            {
                RemoveMoveComponent(obj);
            }
        }
    }
    private void RemoveRotateComponents()
    {
        foreach (GameObject obj in _objectList)
        {
            if (!obj.CompareTag("Ground"))
                RemoveRotateComponent(obj);
        }
    }
    private void RemoveElevateComponents()
    {
        foreach (GameObject obj in _objectList)
        {
            if (!obj.CompareTag("Ground"))
                RemoveElevateComponent(obj);
        }
    }
    private void RemoveMoveComponent(GameObject obj)
    {
        if (obj.TryGetComponent(out PlaneDrag move))
            Destroy(move);
    }
    private void RemoveRotateComponent(GameObject obj)
    {
        if (obj.TryGetComponent(out RotateDrag rotate))
            Destroy(rotate);
    }
    private void RemoveElevateComponent(GameObject obj)
    {
        if (obj.TryGetComponent(out ElevateDrag elevate))
            Destroy(elevate);
    }

    private void OnObjectRemoved(int index)
    {
        RemoveFromRoot(index);
    }

    private void OnObjectAdded()
    {
        int objCount = _objectList.Count;
        AddToRoot(_objectList.Count);
    }

    private void OnContainerUpdate()
    {
        if(_saveManager != null)
        {
            Destroy(_rootObject);
            _objectList = null;
            _rootObject = new GameObject("Room");
            InitializeFixtures();
        }
        
    }

    private void OnMoveToggle(bool value)
    {
        if(value == true)
        {
            AddMoveComponents();
        }
        else
        {
            RemoveMoveComponents();
        }
        _moveState = value;
    }

    private void OnRotateToggle(bool value)
    {
        if (value == true)
        {
            AddRotateComponents();
        }
        else
        {
            RemoveRotateComponents();
        }
        _rotateState = value;
    }

    private void OnElevateToggle(bool value)
    {
        if (value == true)
        {
            AddElevateComponents();
        }
        else
        {
            RemoveElevateComponents();
        }
        _elevateState = value;
    }

    public void FinishedPressed()
    {
        if (SceneManager.GetActiveScene().name == "EditRoomScene")
        {
            for (int i = 0; i < _objectList.Count; i++)
            {
                Transform transform = _objectList[i].transform;
                _roomObjects.SetScaleAt(i, transform.localScale.x, transform.localScale.y, transform.localScale.z);
                _roomObjects.SetPosAt(i, transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
                _roomObjects.SetRotAt(i, transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z);
            }

            SceneManager.LoadScene("SettingScene");
        }
        else if (SceneManager.GetActiveScene().name == "ViewScene")
        {
            SceneManager.LoadScene("StartScene");
        }
    }
    public void SavePressed()
    {
        if (!_saveManager.gameObject.activeSelf)
        {
            for (int i = 0; i < _objectList.Count; i++)
            {
                Transform transform = _objectList[i].transform;
                _roomObjects.SetScaleAt(i, transform.localScale.x, transform.localScale.y, transform.localScale.z);
                _roomObjects.SetPosAt(i, transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
                _roomObjects.SetRotAt(i, transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z);
            }

            _saveManager.gameObject.SetActive(true);
        }
        else
        {
            _saveManager.gameObject.SetActive(false);
        }
    }

    
}
