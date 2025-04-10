using UnityEngine;

public class Spawn_Enemies : MonoBehaviour
{
    private bool isPlayerNearby = false;
    public GameObject Prefab;

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
        if (isPlayerNearby)
        {
            Instantiate(Prefab, new Vector3(0f, 18f, 0f), Quaternion.identity);
        }
    }
}
