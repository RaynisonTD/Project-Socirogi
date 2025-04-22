    using System;
    using System.Reflection;
    using Unity.Behavior;
    using Unity.VisualScripting;
    using UnityEngine;

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

            private void Update()
            {
                if (realTimeStats.health <= 0)
                {
                    Die();
                }
            }

            private void Start()
            {
                player = GameObject.FindGameObjectWithTag("Player");

                if (otherGameObject != null && otherGameObject.TryGetComponent(out BehaviorGraphAgent behaviour))
                {
                    behaviour.BlackboardReference.SetVariableValue("Target", player);
                    
                    try
                    {
                        behaviour.BlackboardReference.SetVariableValue("MoveSpeed", realTimeStats.movespeed);
                    }
                    catch (Exception ex)
                    {
                        Debug.LogWarning($"Failed to set 'MoveSpeed' in blackboard: {ex.Message}");
                    }
                }
                else
                {
                    Debug.LogWarning("BehaviorGraphAgent not found on 'otherGameObject'.");
                }
            }

            void ItemChanges()
            {
                realTimeStats = stats.Clone() as EnemyStats;
                realTimeStatsMax = stats.Clone() as EnemyStats;
            }

            private void Die()
            {
                Debug.Log($"{gameObject.name} is dead!");
                Destroy(gameObject);
            }
        }
        
        
        [System.Serializable] public class EnemyStats : ICloneable
        {

            // Player stats
            public float health;
            public float movespeed;
            public float Damage;
            
       
            //return instance of the player stats as they were before the changes
            public object Clone()
            {
                return this.MemberwiseClone();
            }
        }
    }