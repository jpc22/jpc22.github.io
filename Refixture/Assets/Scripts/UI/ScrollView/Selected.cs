using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Selected : ScrollContent
{
    [SerializeField] protected GameObject _sizeText;
    protected TextMeshProUGUI _sizeTMP;
    protected override bool UseImperial { get => base.UseImperial; set => base.UseImperial = value; }

    protected override void Awake()
    {
        base.Awake();
        _sizeTMP = _sizeText.GetComponent<TextMeshProUGUI>();
    }

    protected override void InitializeContent()
    {
        base.InitializeContent();
        SetSizeText();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        SetSizeText();
    }

    
    protected virtual void SetSizeText()
    {
        Vector3 size = _settingsSO.SelectedFixtureSOList.SizeAt(Index);
        float width = size.z;
        float length = size.x;
        float height = size.y;
        if(UseImperial)
        {
            width = ConvertToInches(width);
            length = ConvertToInches(length);
            height = ConvertToInches(height);
        }
        _sizeTMP.text = "W: " + width.ToString("F2") + " L: " + length.ToString("F2") + " H: " + height.ToString("F2");
    }
    

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    protected override void OnDoubleSelect(PointerEventData eventData)
    {
        _settingsSO.SelectedFixtureSOList.Remove(Index);
    }

    protected override void UpdateSettings()
    {
        base.UpdateSettings();
        SetSizeText();
    }
}
