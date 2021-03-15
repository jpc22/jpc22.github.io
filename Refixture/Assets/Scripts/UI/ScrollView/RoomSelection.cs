using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSelection : Selection
{
    public override void AddToList()
    {
        float scaleZ = _widthValue / _initialWidth;
        float scaleX = _lengthValue / _initialLength;
        float scaleY = _heightValue / _initialHeight;
        _settingsSO.StaticFixtureSOList.Add(ContentSO, _widthValue, _lengthValue, _heightValue, scaleX, scaleY, scaleZ);
    }
}
