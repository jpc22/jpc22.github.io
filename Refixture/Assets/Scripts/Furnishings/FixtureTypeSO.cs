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

	[Tooltip("If true, the object's rear should be against the wall")]
	[SerializeField] private bool _isWallFixture;

	[Tooltip("If true, the object should be on the floor rather than elevated somehow")]
	[SerializeField] private bool _isGroundFixture;

	[Tooltip("If true, the object obeys forces of gravity, unlike something that is fixed in place.")]
	[SerializeField] private bool _isGravityEnabled;

	public string Name => _name;
	public string Description => _description;
	public bool IsLightEmitter => _isLightEmitter;
	public bool IsWallFixture => _isWallFixture;
    public bool IsGroundFixture { get => _isGroundFixture; }
    public bool IsGravityEnabled { get => _isGravityEnabled; }
}
