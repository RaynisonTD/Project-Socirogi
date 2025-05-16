using System;
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable objects/Item")]
public class Item : ScriptableObject
{
    [Header("Only gameplay")]
    public ActionType actionType;
    public ItemType type;

    [Header("Only UI")]
    public bool stackable = true;
    public int healingAmount = 1;

    [Header("Both")]
    public Sprite image;
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