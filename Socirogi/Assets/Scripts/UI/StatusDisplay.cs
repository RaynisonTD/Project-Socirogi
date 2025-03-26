using Stats;
using TMPro;
using UnityEngine;

namespace UI
{
    public class StatusDisplay : MonoBehaviour
    {
        public TextMeshProUGUI statsText;
        

        private PlayerStatsComponent stats;

        void Start()
        {
            // Find and assign the PlayerStats component (or assign in Inspector)
            stats = FindFirstObjectByType<PlayerStatsComponent>(); 

            // Update the UI initially
            UpdateStatsUI();
        }

        void Update()
        {
            // Continuously update the stats UI
            UpdateStatsUI();
        }

        void UpdateStatsUI()
        {
            if (statsText && stats)
            {
                statsText.text = $"Health: {stats.realTimeStats.health}\n" +
                                 $"Mana: {stats.realTimeStats.Mana}\n";

            }
        }
    }
}
