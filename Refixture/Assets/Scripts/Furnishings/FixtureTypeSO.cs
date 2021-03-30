using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This holds the information necessy to define a furnishing
/// </summary>

[CreateAssetMenu(menuName = "Fixture/Fixture Type")]
public class FixtureTypeSO : ScriptableObject
{
	[SerializeField] private string _name;

	[SerializeField] private string _description;

	[Tooltip("If true, the object emits light like a lamp")]
	[SerializeField] private bool _isLightEmitter;

	[Tooltip("If true, the object can have variable height")]
	[SerializeField] private bool _isHeightEnabled;

	[Tooltip("If true, the object's rear should be against the wall")]
	[SerializeField] private bool _isWallFixture;

	public string Name => _name;
	public string Description => _description;
	public bool IsLightEmitter => _isLightEmitter;
	public bool IsHeightEnabled => _isHeightEnabled;
	public bool IsWallFixture => _isWallFixture;
}
