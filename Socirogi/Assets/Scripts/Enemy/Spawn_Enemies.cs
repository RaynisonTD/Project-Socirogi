using System;
using Unity.Mathematics;
using UnityEngine;

public class Spawn_Enemies : MonoBehaviour
{
    private bool isPlayerNearby = false;
    public GameObject Enemy;
    private Transform vloer;
    public GameObject muur;
    private GameObject enemy;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            SpawnEnemy();
        }
    }

    public void SpawnEnemy()
    {
        vloer = GameObject.Find("enemy_spawn").transform;
        Debug.Log(vloer.position);
        if (isPlayerNearby)
        {
            Instantiate(Enemy, vloer.position, Quaternion.identity);
            Block_Room();

        }
    }

    private void Update()
    {
        enemy = GameObject.Find("Enemy(Clone)");
        if (enemy == null)
        {
            UnBlock_Room();
        }
    }

    public void Block_Room()
    {
        Instantiate(muur, new Vector3(10, 5, 20), Quaternion.identity);
        Instantiate(muur, new Vector3(22, 5, 9), Quaternion.Euler(0, 90, 0));

    }

    public void UnBlock_Room()
    {
        GameObject[] alleObjecten = FindObjectsOfType<GameObject>();

        foreach (GameObject obj in alleObjecten)
        {
            if (obj.name == "de_muur(Clone)")
            {
                Destroy(obj);
            }
        }
    }
}
