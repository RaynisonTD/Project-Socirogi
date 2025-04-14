using System;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Enemies : MonoBehaviour
{
    [System.Serializable]
    //klas lijst voor de enemy ding die je rechts gaat zienin je inspector
    public class EnemySpawn
    {
        public GameObject enemyPrefab;
        public Transform spawnPoint;
        [HideInInspector] public GameObject spawnedInstance; 
    }

    [Header("Enemy Spawns")]
    public List<EnemySpawn> enemies = new List<EnemySpawn>();

    private bool isPlayerNearby = false;
    private bool enemiesSpawned = false;

    //wanneer de player colide met de trigger collsion spawn de enemies in
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !enemiesSpawned)
        {
            isPlayerNearby = true;
            SpawnEnemies();
        }
    }
    
    public void SpawnEnemies()
    {
        if (!isPlayerNearby) return;

        //voor elke enemy die er in de lijst zit en ook gevuld is spawn die enemy op het beschreven punt waar die bij hoort
        foreach (var enemy in enemies)
        {
            if (enemy.enemyPrefab != null && enemy.spawnPoint != null)
            {
                enemy.spawnedInstance = Instantiate(enemy.enemyPrefab, enemy.spawnPoint.position, enemy.spawnPoint.rotation);
            }
        }

        enemiesSpawned = true;
    }





    
}