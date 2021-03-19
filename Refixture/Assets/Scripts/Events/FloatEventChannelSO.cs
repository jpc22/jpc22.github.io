using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Float Event Channel")]
public class FloatEventChannelSO : ScriptableObject
{
    public UnityAction<float> OnEventRaised;

	public void RaiseEvent(float so)
	{
		if (OnEventRaised != null)
			OnEventRaised.Invoke(so);
	}
}
