using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleControl : MonoBehaviour
{
    [SerializeField] private BoolEventChannelSO _updateChannel;

    private void Start()
    {
        bool isDefault = gameObject.GetComponent<Toggle>().isOn;
        if (isDefault)
            _updateChannel.RaiseEvent(isDefault);
    }
    public void ToggleChanged()
    {
        _updateChannel.RaiseEvent(gameObject.GetComponent<Toggle>().isOn);
    }
}
