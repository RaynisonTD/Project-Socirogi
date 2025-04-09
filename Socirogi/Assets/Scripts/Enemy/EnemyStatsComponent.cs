using System;
using UnityEngine;

namespace Stats
{
    
    [Serializable]
    public class EnemyStats
    {
        public float health = 100f;
        public float damage = 5f;
        public float moveSpeed = 2f;
    }

    public class EnemyStatsComponent : MonoBehaviour
    {
        public EnemyStats realTimeStats = new EnemyStats();
        public EnemyStats baseStats = new EnemyStats(); 

        [SerializeField] private EnemyHealthBar healthBarPrefab; 
        private EnemyHealthBar healthBarInstance;

        
        
        void Start()
        {
            if (healthBarPrefab != null)
            {
                healthBarInstance = Instantiate(healthBarPrefab, transform);
                healthBarInstance.transform.localPosition = new Vector3(0, 2, 0);

                healthBarInstance.transform.SetParent(transform); 
                healthBarInstance.cam = Camera.main;
                
                
            }

            // Zet de basis stats (meestal hetzelfde als realTimeStats)
            baseStats.health = realTimeStats.health;
        }

        void Update()
        {
            if (healthBarInstance != null)
            {
                healthBarInstance.SetHealth(realTimeStats.health, baseStats.health);
            }
            Debug.Log("Updating health: " + realTimeStats.health);

            if (realTimeStats.health <= 0)
            {
                Destroy(gameObject); 
            }
        }


        public void TakeDamage(float damageAmount)
        {
            realTimeStats.health -= damageAmount;
            if (realTimeStats.health < 0) realTimeStats.health = 0;
        }
    }
}
