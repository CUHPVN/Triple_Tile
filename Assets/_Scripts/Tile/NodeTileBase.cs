using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tile", menuName = "ScriptableObjects/Tile", order = 1)]
public class NodeTileBase : ScriptableObject
{
    public List<Sprite> sprites = new();
    public List<SpriteList> spritesBase = new();
    public void SetTileSprite(TileSprite tileSprite)
    {
        sprites = spritesBase[(int)tileSprite].sprites;
    }
}
[System.Serializable]
public class SpriteList
{
    public List<Sprite> sprites;
}

public enum TileSprite
{
    Weapon,
    Equipment,
    Fruit,
}
