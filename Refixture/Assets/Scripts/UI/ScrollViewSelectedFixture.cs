using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScrollViewSelectedFixture : MonoBehaviour
{
    
    [SerializeField] private GameObject _selectionUI;
    [SerializeField] private SelectedFixtureListSO _selectedListSO;
    [SerializeField] private VoidEventChannelSO _selectedListUpdateChannel;
    [SerializeField] private ObjEventChannelSO _selectedRemovedChannel;
    [SerializeField] private List<GameObject> _selectedUIList;

    private List<FurnishingSO> _selectedList;

    void Awake()
    {
        _selectedListUpdateChannel.OnEventRaised += UpdateList;
        _selectedRemovedChannel.OnEventRaised += SelectedRemoved;
        UpdateList();
    }

    public void UpdateList()
    {
        foreach (GameObject ui in _selectedUIList)
            Destroy(ui);
        _selectedUIList = new List<GameObject>();

        _selectedList = _selectedListSO.getCopy();

        RectTransform thisRect = gameObject.GetComponent<RectTransform>();
        float y = 0;
        thisRect.sizeDelta = new Vector3(thisRect.sizeDelta.x, y);

        foreach (var selected in _selectedList)
        {
            GameObject selectionUI = Instantiate(_selectionUI, this.gameObject.transform);
            _selectedUIList.Add(selectionUI);
            GameObject nameTextUI = selectionUI.transform.GetChild(0).gameObject;
            nameTextUI.GetComponent<TextMeshProUGUI>().text = selected.Name;

            RectTransform selectionRect = selectionUI.GetComponent<RectTransform>();
            selectionRect.anchoredPosition = new Vector3(selectionRect.anchoredPosition.x, y);
            y -= selectionRect.rect.height;

            thisRect.sizeDelta = new Vector3(thisRect.sizeDelta.x, Mathf.Abs(y));
        }
    }

    public void SelectedRemoved(GameObject obj)
    {
        int index = -1;
        for(int i = 0; i < _selectedUIList.Count; i++)
        {
            if(GameObject.ReferenceEquals(obj, _selectedUIList[i]))
            {
                index = i;
            }
        }
        if (index != -1)
            _selectedListSO.Remove(index);
    }
}
