using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleControl : MonoBehaviour
{
    [SerializeField] private BoolEventChannelSO _updateChannel;

    public void ToggleChanged()
    {
        _updateChannel.RaiseEvent(gameObject.GetComponent<Toggle>().isOn);
    }
}
