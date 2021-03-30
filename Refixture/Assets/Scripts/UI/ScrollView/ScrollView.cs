using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class ScrollView : MonoBehaviour
{
    [SerializeField] protected GameObject _contentPrefab;
    [SerializeField] protected FixtureContainerSO _fixtureContainer;
    protected VoidEventChannelSO _updateChannel;
    protected RectTransform _rectTransform;
    protected List<GameObject> _contents;
    protected float _contentHeight;
    protected float _contentDelta;

    public virtual FixtureContainerSO FixtureContainer
    {
        get => _fixtureContainer; set
        {
            _fixtureContainer = value;
            DestroyContents();
            UpdateContents();
            Start();
        }
    }

    public virtual void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _contents = new List<GameObject>();
    }

    public virtual void OnEnable()
    {
        _updateChannel = _fixtureContainer.UpdateChannel;
        if(_updateChannel != null)
            _updateChannel.OnEventRaised += OnContainerUpdated;
    }

    public virtual void Start()
    {
        _contentDelta = _contentPrefab.GetComponent<RectTransform>().rect.height;
        _contentHeight = 0;
        UpdateContents();
    }

    public virtual void OnContainerUpdated()
    {
        UpdateContents();
    }

    public virtual void UpdateContents()
    {
        DestroyContents();
        _contentHeight = 0;
        SpawnContent();
    }

    protected virtual void SpawnContent()
    {
        List<FixtureSO> contentList = _fixtureContainer.Fixtures;
        for (int i = 0; i < contentList.Count; i++)
        {
            GameObject content = Instantiate(_contentPrefab, transform);

            _contents.Add(content);

            // set position of content in list
            RectTransform contentRect = content.GetComponent<RectTransform>();
            contentRect.anchoredPosition = new Vector3(contentRect.anchoredPosition.x, _contentHeight);
            _contentHeight -= _contentDelta;
            // Match scroll view to the total size
            _rectTransform.sizeDelta = new Vector3(_rectTransform.sizeDelta.x, Mathf.Abs(_contentHeight));

            // pass FixtureSO to content
            content.GetComponent<ScrollContent>().Index = i;
            content.GetComponent<ScrollContent>().ContentSO = contentList[i];
        }
        
    }

    public virtual void OnDisable()
    {
        if (_updateChannel != null)
            _updateChannel.OnEventRaised -= OnContainerUpdated;
        DestroyContents();
    }

    public virtual void DestroyContents()
    {
        if(_contents != null || _contents.Count > 0)
        {
            foreach (GameObject content in _contents) Destroy(content);
            _contents.Clear();
        }
    }
}
