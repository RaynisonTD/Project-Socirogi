using UnityEngine;

namespace EnemyAI.Enemy
{
    public class DistanceTrigger : MonoBehaviour
    {
        public DistanceCheck1 distanceCheck;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                distanceCheck?.SetTargetInRange(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                distanceCheck?.SetTargetInRange(false);
            }
        }
    }
}
