using UnityEngine;
using Stats;

public class FireballProjectile : MonoBehaviour
{
    [SerializeField] private float damage = 20f;
    [SerializeField] private float lifetime = 5f;

    void Start()
    {
        Destroy(gameObject, lifetime); // Vernietig de vuurbal na 5 seconden
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out EnemyStatsComponent enemyStats))
        {
            enemyStats.realTimeStats.health -= damage;
            Debug.Log($"Fireball hit! Dealt {damage} damage. Enemy HP is now: {enemyStats.realTimeStats.health}");
        }
        else
        {
            Debug.Log("Fireball hit something, but it wasn't an enemy.");
        }

        Destroy(gameObject); // Vernietig de vuurbal bij impact
    }

}