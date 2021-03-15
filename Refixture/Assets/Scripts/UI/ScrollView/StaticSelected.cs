using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StaticSelected : Selected
{
    protected override void SetSizeText()
    {
        Vector3 size = _settingsSO.StaticFixtureSOList.SizeAt(Index);
        float width = size.z;
        float length = size.x;
        float height = size.y;
        if (UseImperial)
        {
            width = ConvertToInches(width);
            length = ConvertToInches(length);
            height = ConvertToInches(height);
        }
        _sizeTMP.text = "W: " + width.ToString("F2") + " L: " + length.ToString("F2") + " H: " + height.ToString("F2");
    }
    protected override void OnDoubleSelect(PointerEventData eventData)
    {
        _settingsSO.StaticFixtureSOList.Remove(Index);
    }
}
