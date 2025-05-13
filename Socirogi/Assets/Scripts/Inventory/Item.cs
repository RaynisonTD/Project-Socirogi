using System;
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable objects/Item")]
public class Item : ScriptableObject
{
    [Header("Only gameplay")]
    public ActionType actionType;
    public ItemType type;
    public GameObject prefab;

    [Header("Only UI")]
    public bool stackable = true;

    [Header("Both")]
    public Sprite image;
    
    public virtual void Use(GameObject user)
    {
        // Basisklasse doet niets
        Debug.Log($"Item '{name}' used, but no effect defined.");
    }
}


public enum ItemType
{
    Potion,
    Attack
}

public enum ActionType
{
    Use,
    Attack
}