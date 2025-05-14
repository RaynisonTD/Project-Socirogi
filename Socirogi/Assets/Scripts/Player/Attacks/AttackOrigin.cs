using Inventory_System;
using Player;
using UnityEngine;
using System.Collections;

public class AttackOrigin : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float projectileSpeed = 20f;
    [SerializeField] private float attackCooldown = 1f;

    [SerializeField] private Item requiredItem; // <== Dit moet overeenkomen met jouw vuurbal item in de inventory

    private bool canAttack = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryAttack();
        }
    }

    void TryAttack()
    {
        if (!canAttack) return;

        //hier kijkt die of de jusite item geselect is anedrs heeft het geen nut
        Item selectedItem = InventoryManager.Instance.GetSelectedItem(false);
        if (selectedItem != requiredItem) return;

        StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
        canAttack = false;
        //

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

}