using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Furnishing Event Channel")]
public class FurnishingEventChannelSO : ScriptableObject
{
    public UnityAction<FurnishingSO> OnEventRaised;

	public void RaiseEvent(FurnishingSO so)
	{
		if (OnEventRaised != null)
			OnEventRaised.Invoke(so);
	}
}
