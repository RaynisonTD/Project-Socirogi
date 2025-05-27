using Unity.Behavior;
using UnityEngine;

namespace EnemyAI.Enemy
{
    public class DistanceCheck1 : MonoBehaviour
    {
        public BehaviorGraphAgent agent;

        public string blackboardBoolName = "TargetInRange";

        public void SetTargetInRange(bool value)
        {
            if (agent != null)
            {
                agent.SetVariableValue(blackboardBoolName, value);
            }
            else
            {
                Debug.LogWarning("error");
            }
        }
    }
}
    
