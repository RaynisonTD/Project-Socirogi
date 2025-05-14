using System;
using System;
using System.Net.Mime;
using UnityEngine;

using UnityEngine.UI;

[CreateAssetMenu(menuName = "Scriptable objects/HealItem")]
public class HealItem : ScriptableObject
{
    [Header("Only gameplay")]
    public GameObject prefab;

    [Header("Only UI")]
    public bool stackable = true;

    [Header("Both")]
    public Sprite image;
    public int healingAmount = 20;
    
}

