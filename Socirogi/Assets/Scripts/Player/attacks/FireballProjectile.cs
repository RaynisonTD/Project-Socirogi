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
            enemyStats.TakeDamage(damage); // Roep de TakeDamage functie aan om de vijand te beschadigen
        }

        Destroy(gameObject); // Vernietig de vuurbal bij impact
    }
}