using UnityEngine;
using Unity.Behavior;

public class EnemyAttackRange : MonoBehaviour
{
    private BehaviorGraphAgent agent;

    private void Start()
    {
        agent = GetComponent<BehaviorGraphAgent>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered attack range!");
            agent?.BlackboardReference.SetVariableValue("IsInAttackRange", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited attack range!");
            agent?.BlackboardReference.SetVariableValue("IsInAttackRange", false);
        }
    }
}
