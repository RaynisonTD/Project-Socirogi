using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using Stats;

public class HealingItem : MonoBehaviour
{
    private Text hpText;
    private PlayerStatsComponent playerStats;

    private void Start()
    {
        hpText = GetComponent<Text>();

        
    }

    public IEnumerator Heal(int healingAmount )
    {
        playerStats = FindFirstObjectByType<PlayerStatsComponent>();
        float currentHealth = playerStats.realTimeStats.health;
        playerStats.realTimeStats.health += healingAmount;
        
        yield return null;
    }

}