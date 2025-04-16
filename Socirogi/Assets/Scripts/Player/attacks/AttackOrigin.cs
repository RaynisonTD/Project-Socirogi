using UnityEngine;
using Player;
using System.Collections;

public class AttackOrigin : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float projectileSpeed = 20f;
    [SerializeField] private float attackCooldown = 1f;

    private bool canAttack = true;

    void Start()
    {
        PlayerController.OnAttackInput += TryAttack;
    }

    void TryAttack()
    {
        if (!canAttack) return;

        StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
        canAttack = false;

        Vector3 spawnPos = firePoint.position + firePoint.forward * 0.5f;
        GameObject projectile = Instantiate(projectilePrefab, spawnPos, firePoint.rotation);

        Collider projectileCollider = projectile.GetComponent<Collider>();
        Collider playerCollider = GetComponentInParent<Collider>();

        if (projectileCollider != null && playerCollider != null)
        {
            Physics.IgnoreCollision(projectileCollider, playerCollider);
        }

        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = firePoint.forward * projectileSpeed;
        }

        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }


    void OnDestroy()
    {
        PlayerController.OnAttackInput -= TryAttack;
    }
}