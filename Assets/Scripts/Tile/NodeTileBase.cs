using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tile", menuName = "ScriptableObjects/Tile", order = 1)]
public class NodeTileBase : ScriptableObject
{
    public List<Sprite> sprites= new();
}
