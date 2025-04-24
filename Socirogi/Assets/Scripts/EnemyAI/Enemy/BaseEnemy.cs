using Unity.Behavior;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{

    public EnemyStats stats;

    [SerializeField] private float baseSpeed = 5f;
    
    private float currentSpeed;

    private Transform target;
    private Rigidbody rb;
    private BehaviorGraphAgent agent;

    protected virtual void Awake()
    {
        currentSpeed = baseSpeed * stats.speedMultiplier;


        target = GameObject.FindGameObjectWithTag("Player").transform;

        rb = GetComponent<Rigidbody>();
        agent = GetComponent<BehaviorGraphAgent>();

        agent.BlackboardReference.SetVariableValue("Speed", currentSpeed);
    }
}

