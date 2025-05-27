using Unity.Behavior;
using UnityEngine;

namespace EnemyAI.Enemy
{
    public partial class DistanceCheck : MonoBehaviour
    {
        public Transform target;
        public float maxDistance = 10f;
        public LayerMask detectionLayer;

        public BehaviorGraphAgent agent;

        void Update()
        {
            if (agent == null || target == null)
                return;

            bool isInRange = false;

            Vector3 direction = (target.position - transform.position).normalized;

            if (Physics.Raycast(transform.position, direction, out RaycastHit hit, maxDistance, detectionLayer))
            {
                isInRange = true;
                Debug.DrawRay(transform.position, direction * maxDistance, Color.red);
                agent.BlackboardReference.SetVariableValue("IsInRange", isInRange);

            }

            else

            {
                Debug.DrawRay(transform.position, transform.forward * maxDistance, Color.green);

                agent = GetComponent<BehaviorGraphAgent>();
                isInRange = false;
                agent.BlackboardReference.SetVariableValue("IsInRange", isInRange);
            }
        }
    

    }
}  