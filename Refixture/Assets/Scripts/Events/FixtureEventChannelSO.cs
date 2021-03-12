using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Furnishing Event Channel")]
public class FixtureEventChannelSO : ScriptableObject
{
    public UnityAction<FixtureSO> OnEventRaised;

	public void RaiseEvent(FixtureSO so)
	{
		if (OnEventRaised != null)
			OnEventRaised.Invoke(so);
	}
}
