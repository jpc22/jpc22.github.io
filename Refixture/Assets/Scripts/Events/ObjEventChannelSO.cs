using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Game Object Event Channel")]
public class ObjEventChannelSO : ScriptableObject
{
	public UnityAction<GameObject> OnEventRaised;

	public void RaiseEvent(GameObject so)
	{
		if (OnEventRaised != null)
			OnEventRaised.Invoke(so);
	}
}
