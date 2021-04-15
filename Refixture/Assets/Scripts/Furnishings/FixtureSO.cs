using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// This holds the information necessy to define a furnishing
/// </summary>

[CreateAssetMenu(menuName = "Fixture/Fixture Object")]
public class FixtureSO : ScriptableObject
{
	[SerializeField] private string _name;
	[SerializeField] private string _prefabName;
	[SerializeField] private FixtureTypeSO[] _types;
	[SerializeField] private string[] _functionalSides;
	[SerializeField] private RenderTexture _previewTexture;
	private RenderTexture _thisRT;
	private bool _isWallFixture;
	private bool _isGroundFixture;
	private bool _isLightEmitter;
	private bool _isGravityEnabled;
	private bool _isWallFixtureInit = false;
	private bool _isGroundFixtureInit = false;
	private bool _isLightEmitterInit = false;
	private bool _isGravityEnabledInit = false;

	public string Name => _name;
	public FixtureTypeSO[] Types => _types;

    public GameObject GetPrefab3d()
    {
		return Resources.Load<GameObject>(_prefabName);
    }
    public string[] FunctionalSides => _functionalSides;
    public RenderTexture PreviewTexture
    {
        get
        {
			if (_thisRT == null)
			{
				_thisRT = new RenderTexture(TexturePreset);
			}
			
            return _thisRT;
        }
    }
	public RenderTexture TexturePreset
    {
		get
        {
			if (_previewTexture == null)
            {
				_previewTexture = Resources.Load<RenderTexture>("RenderTexture");
            }
			return _previewTexture;
        }
    }

    public bool IsWallFixture
    {
        get
        {
			if (!_isWallFixtureInit)
            {
				_isWallFixture = CheckIfWallFixture();
				_isWallFixtureInit = true;
            }
            return _isWallFixture;
        }
    }
    public bool IsGroundFixture
    {
        get
        {
			if (!_isGroundFixtureInit)
            {
				_isGroundFixture = CheckIfGroundFixture();
				_isGroundFixtureInit = true;
            }
            return _isGroundFixture;
        }
    }
    public bool IsLightEmitter
    {
        get
        {
			if (!_isLightEmitterInit)
            {
				_isLightEmitter = CheckIfLightEmitter();
				_isLightEmitterInit = true;
            }
            return _isLightEmitter;
        }
    }

    public bool IsGravityEnabled
    {
        get
        {	if (!_isGravityEnabledInit)
            {
				_isGravityEnabled = CheckIfGravityEnabled();
				_isGravityEnabledInit = true;
            }
            return _isGravityEnabled;
        }
    }

    /// <summary>
    /// Checks if the furniture is of the specified type
    /// </summary>
    /// <param name="type">The Name field of the SO</param>
    /// <returns>true if type is found</returns>
    public bool IsOfType(string type)
    {
		if (_types == null || _types.Length == 0)
		{
			return false;
		}
		else if (CheckForType())
        {
			return true;
        }
        else
        {
			return false;
        }
		bool CheckForType()
		{
			for (int i = 0; i < _types.Length; i++)
			{
				if (_types[i].Name == type) return true;
			}
			return false;
		}
	}

	public bool IsOfType(FixtureTypeSO type)
	{
		if (_types == null || _types.Length == 0)
		{
			return false;
		}
		else if (CheckForType())
		{
			return true;
		}
		else
		{
			return false;
		}
		bool CheckForType()
		{
			for (int i = 0; i < _types.Length; i++)
			{
				if (_types[i].Name == type.Name) return true;
			}
			return false;
		}
	}
	
	private bool CheckIfWallFixture()
    {
		bool value = false;
		for (int i = 0; i < _types.Length; i++)
        {
			if (_types[i].IsWallFixture)
            {
				value = true;
				break;
            }
        }
		return value;
    }
	private bool CheckIfGroundFixture()
	{
		bool value = false;
		for (int i = 0; i < _types.Length; i++)
		{
			if (_types[i].IsGroundFixture)
			{
				value = true;
				break;
			}
		}
		return value;
	}
	private bool CheckIfLightEmitter()
    {
		bool value = false;
		for (int i = 0; i < _types.Length; i++)
		{
			if (_types[i].IsLightEmitter)
			{
				value = true;
				break;
			}
		}
		return value;
	}
	private bool CheckIfGravityEnabled()
    {
		bool value = false;
		for (int i = 0; i < _types.Length; i++)
		{
			if (_types[i].IsGravityEnabled)
			{
				value = true;
				break;
			}
		}
		return value;
	}
}


