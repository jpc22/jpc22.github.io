using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Fixture/Furnishing Object Container")]
public class FurnishingContainerSO : ScriptableObject
{
    [SerializeField] string _name;
    [SerializeField] List<FurnishingSO> _furnishings;

    public string Name => _name;
    public List<FurnishingSO> Furnishings { get => _furnishings; set => _furnishings = value; }
}
