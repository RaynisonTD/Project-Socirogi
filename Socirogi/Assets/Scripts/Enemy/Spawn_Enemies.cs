using UnityEngine;

public class Spawn_Enemies : MonoBehaviour
{
    private bool isPlayerNearby = false;
    public GameObject Prefab;
    private Transform vloer;

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
            Instantiate(Prefab, vloer.position, Quaternion.identity);
            
        }
    }
}
