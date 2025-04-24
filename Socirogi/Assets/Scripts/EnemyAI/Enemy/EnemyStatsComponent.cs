using System;
using UnityEngine;
using Unity.Behavior;
using Unity.VisualScripting;

namespace Stats
{
    public class EnemyStatsComponent : MonoBehaviour
    {
        public GameObject otherGameObject;
        private GameObject player;
        
        [SerializeField] private EnemyStats stats;
        [HideInInspector] public EnemyStats realTimeStats;
        [HideInInspector] public EnemyStats realTimeStatsMax;

        private void Awake()
        {
            ItemChanges();
        }

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");

            // Debug check on realTimeStats
            if (realTimeStats == null)
            {
                Debug.LogWarning("realTimeStats is null in Start!");
            }
            
            if (otherGameObject != null && otherGameObject.TryGetComponent(out BehaviorGraphAgent behaviour))
            {
                Debug.Log("BehaviorGraphAgent found on otherGameObject.");

                var blackboard = behaviour.BlackboardReference;

                if (blackboard == null)
                {
                    Debug.LogWarning("BlackboardReference is null!");
                }
                else
                {
                    Debug.Log("Setting blackboard variables...");
                    blackboard.SetVariableValue("Target", player);
                }
            }
            else
            {
                Debug.LogWarning("BehaviorGraphAgent not found on 'otherGameObject'.");
            }
        }

        private void Update()
        {
            if (realTimeStats != null && realTimeStats.health <= 0)
            {
                Die();
            }
        }

        private void ItemChanges()
        {
            realTimeStats = stats.Clone() as EnemyStats;
            realTimeStatsMax = stats.Clone() as EnemyStats;

            Debug.Log("Stats cloned into realTimeStats.");
        }

        private void Die()
        {
            Debug.Log($"{gameObject.name} is dead!");
            Destroy(gameObject);
        }
    }

    [System.Serializable]
    public class EnemyStats : ICloneable
    {
        public float health;
        public float Damage;

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
