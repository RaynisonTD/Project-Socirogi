using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "Scriptable Objects/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    [Range(0f, 2f)]public float healthMultiplier = 1f;
    [Range(0f, 2f)]public float damageMultiplier = 1f;
    [Range(0f, 2f)]public float speedMultiplier = 1f;
    
    public float attackRange = 3f;
}
