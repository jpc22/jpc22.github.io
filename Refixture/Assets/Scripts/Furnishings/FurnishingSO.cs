using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// This holds the information necessy to define a furnishing
/// </summary>

[CreateAssetMenu(menuName = "Fixture/Furnishing Object")]
public class FurnishingSO : ScriptableObject
{
	[SerializeField] private string _name;

	[SerializeField] private FurnishingTypeSO[] _types;

	[SerializeField] private GameObject _prefab3d;

	[SerializeField] private GameObject _prefab2d;

	[SerializeField] private string[] _functionalSides;

	[SerializeField] private RenderTexture _previewTexture;

	public string Name => _name;
	public FurnishingTypeSO[] Types => _types;
	public GameObject Prefab3d => _prefab3d;
	public GameObject Prefab2d => _prefab2d;
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

	public bool IsOfType(FurnishingTypeSO type)
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

	public Texture2D getPreviewImage()
    {
		if(_prefab3d != null)
        {
			Texture2D a = AssetPreview.GetMiniThumbnail(_prefab3d);
			return AssetPreview.GetMiniThumbnail(_prefab3d);
			
		}
		else if(_prefab2d != null)
        {
			return AssetPreview.GetMiniThumbnail(_prefab3d);
		}
		return null;
    }
}


