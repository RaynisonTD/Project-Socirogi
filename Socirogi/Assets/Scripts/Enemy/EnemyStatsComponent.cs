using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Stats
{
    
    public class EnemyStatsComponent : MonoBehaviour
    {
        // instance of the original stats of the player 
        [SerializeField] private EnemyStats stats;
        
        // instance of realtime stats of the player
        [HideInInspector] public EnemyStats realTimeStats;
        
        private void Awake()
        {
            ItemChanges();
        }
        /// <summary>
        /// apply any changes to player stats if they are present
        /// </summary>
        void ItemChanges()
        {
            // clone the original stats of the player in case of veranderingen
            realTimeStats = stats.Clone() as EnemyStats;
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