using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectedFixture : MonoBehaviour
{
    [SerializeField] private GameObject nameText;
    [SerializeField] private GameObject removeButton;
    [SerializeField] private string _name;
    [SerializeField] private ObjEventChannelSO _selectedRemovedChannel;
    public string Name
    {
        get => _name; set
        {
            _name = value;
            nameText.GetComponent<TextMeshProUGUI>().text = value;
        }
    }

    public void SelectedFixtureRemoved()
    {
        _selectedRemovedChannel.RaiseEvent(this.gameObject);
    }
}
