using System.Collections;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float attackRange = 5f;
    public float attackCooldown = 3f;
    public float attackWarningTime = 1f;


    public float minSpeed = 6f;
    public float maxSpeed = 8f;
    public int minDamage = 7;
    public int maxDamage = 20;
    public int minHealth = 150;
    public int maxHealth = 200;


    public float chaseStartDelay = 1f;

    private Transform _player;
    private float _moveSpeed;
    private int _damage;
    public float health;

    private float _lastAttackTime;
    private float _playerSeenTime;
    private bool _hasStartedChasing = false;
    private bool _isAttacking = false;

    void Start()
    {
            
            
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj == null)
        {
            Debug.LogWarning("Player not found in scene!");
            enabled = false;
            return;
        }

        _player = playerObj.transform;
        _moveSpeed = Random.Range(minSpeed, maxSpeed);
        _damage = Random.Range(minDamage, maxDamage + 1);
        health = Random.Range(minHealth, maxHealth + 1);
        _lastAttackTime = -attackCooldown;
    }

    void Update()
    {
        if (_player == null) return;

        float distance = Vector3.Distance(transform.position, _player.position);

        FacePlayer();

        
        if (!_hasStartedChasing)
        {
            _playerSeenTime += Time.deltaTime;
            if (_playerSeenTime >= chaseStartDelay)
                _hasStartedChasing = true;
        }

        if (distance <= attackRange)
        {
            TryAttack();
        }
        else if (_hasStartedChasing)
        {
            ChasePlayer();
        }

        Debug.DrawRay(transform.position, transform.forward * attackRange, Color.red);
    }

    void FacePlayer()
    {
        Vector3 direction = (_player.position - transform.position).normalized;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    void ChasePlayer()
    {
        Vector3 direction = (_player.position - transform.position).normalized;
        direction.y = 0f;
        transform.position += direction * (_moveSpeed * Time.deltaTime);
    }

    void TryAttack()
    {
        if (_isAttacking || Time.time < _lastAttackTime + attackCooldown)
            return;

        StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
        _isAttacking = true;
        Debug.Log("!!");
        yield return new WaitForSeconds(attackWarningTime);

        Attack();
        _lastAttackTime = Time.time;
        _isAttacking = false;
    }

    void Attack()
    {
        Debug.Log($"Enemy attacks the player for {_damage} damage!");
        // TODO: Hook in your player health/damage system here
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
            Die();
    }

    void Die()
    {
        Debug.Log("Enemy died.");
        Destroy(gameObject);
    }
}