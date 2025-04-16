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

    public GameObject Wall;
    public GameObject Chest;
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
        Block_Room();
        enemiesSpawned = true;
    }

    public void Block_Room()
    {
        BoxCollider box = GetComponent<BoxCollider>();
        if (box == null)
        {
            Debug.LogError("Geen BoxCollider gevonden op de kamer!");
            return;
        }

        Vector3 center = box.center + transform.position;
        Vector3 size = box.size;

        // Posities van muren (voorkant, achterkant, links, rechts)
        Vector3[] posities = new Vector3[]
        {
            center + new Vector3(0, 0, size.z / 2),     // Voor
            center + new Vector3(0, 0, -size.z / 2),    // Achter
            center + new Vector3(size.x / 2, 0, 0),     // Rechts
            center + new Vector3(-size.x / 2, 0, 0)     // Links
        };

        Vector3[] rotaties = new Vector3[]
        {
            new Vector3(0, 0, 0), 
            new Vector3(0, 180, 0), 
            new Vector3(0, 90, 0),  
            new Vector3(0, -90, 0)  
        };

        float muurDikte = 1f;
        float muurHoogte = 7f;

        for (int i = 0; i < posities.Length; i++)
        {
            GameObject nieuweMuur = Instantiate(Wall, posities[i], Quaternion.Euler(rotaties[i]), this.transform);

            // Optioneel: schaal aanpassen zodat hij de kamer breedte/hoogte dekt
            nieuweMuur.transform.localScale = new Vector3(size.x, muurHoogte, muurDikte);
        }
    }


    public void UnBlock_Room()
    {
        //pak alle objecten
        GameObject[] alleObjecten = FindObjectsOfType<GameObject>();

        //ga door elk object heen
        foreach (GameObject obj in alleObjecten)

        {
            //als het object naam deze naam heeft wordt het verwijderd.
            if (obj.name == "de_muur(Clone)")
            {
                Destroy(obj);
            }
        }
        Spawn_Reward();
    }


    public void Spawn_Reward()
    {
        BoxCollider box = GetComponent<BoxCollider>();
        Vector3 center = box.center + transform.position;
        center.y = center.y + 2;
        
        Instantiate(Chest, center, Quaternion.identity);
    }
    void Update()
    {
        if (enemiesSpawned)
        {
            bool allEnemiesDead = true;
            foreach (var enemy in enemies)
            {
                if (enemy.spawnedInstance != null)
                {
                    allEnemiesDead = false;
                    break;
                }
            }

            if (allEnemiesDead)
            {
                UnBlock_Room();
                enemiesSpawned = false;
            }
        }
    }
}