using Unity.Behavior;
using UnityEngine;

namespace Basestats
{
    
}
public class BaseEnemy : MonoBehaviour
{

    public EnemyStats stats;

   [SerializeField] private float baseHealth = 100f;
   [SerializeField] private float baseDamage = 20f;
   [SerializeField]private float baseSpeed = 5f;

    private float currentHealth;
    private float attckDamage;
    private float currentSpeed;

    private Transform target;
    private Rigidbody rb;
    private BehaviorGraphAgent agent;
    protected virtual void Awake()
    {
        currentHealth = baseHealth * stats.healthMultiplier;
        currentSpeed = baseSpeed * stats.speedMultiplier;
        attckDamage = baseDamage * stats.damageMultiplier;

        target = GameObject.FindGameObjectWithTag("Player").transform;

        rb = GetComponent<Rigidbody>();
        agent = GetComponent<BehaviorGraphAgent>();
        
        agent.BlackboardReference.SetVariableValue("Speed", currentSpeed);
    }

    public virtual void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {

        Debug.Log(gameObject.name + "has died");
        Destroy(gameObject);
    }
}
    
