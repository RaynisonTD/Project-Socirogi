using System;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable objects/Item")]
public class Item : ScriptableObject
{
    [Header("Only gameplay")]
     public ActionType actionType;
     public Vector2Int range = new Vector2Int(5, 4);
     public ItemType type; 
     
     [Header("Only UI")] 
    public bool stackable = true;
    
    [Header("Both")]
    public Sprite image;
}

public enum ItemType
{
    BuildingBlock,
    Tool
}

public enum ActionType
{
    Dig,
    Mine,
    Attack
}