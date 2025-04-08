using UnityEngine;
using Player;

public class AttackOrigin : MonoBehaviour
{
    [SerializeField] private Cooldown cooldown; 
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float projectileSpeed = 20f;

    void Start()
    {
        PlayerController.OnAttackInput += Attack;
    }

    void Attack()
    {
        // Spawn de vuurbal
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Zorg dat de vuurbal beweegt
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = firePoint.forward * projectileSpeed; // Beweeg in de richting van de speler
        }

        cooldown.StartCoolDown();
    }

    private void OnDestroy()
    {
        PlayerController.OnAttackInput -= Attack;
    }
}