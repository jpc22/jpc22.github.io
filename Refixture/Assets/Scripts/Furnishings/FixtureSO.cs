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

	public string Name => _name;
	public FixtureTypeSO[] Types => _types;

    public GameObject GetPrefab3d()
    {
		return Resources.Load<GameObject>(_prefabName);
    }
    public string[] FunctionalSides => _functionalSides;
    public RenderTexture PreviewTexture => _previewTexture;

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

	public bool IsWallFixture()
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

	public bool IsLightEmitter()
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

}


