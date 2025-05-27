using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using UnityEngine.Serialization;
using Composite = Unity.Behavior.Composite;

namespace Enemy
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "PlayerInRangeCondition", story: "[Self] chases the [target] till he's in [range]",
        category: "Flow", id: "db114dce0eccce95f7fe98db617d6c7d")]
    public partial class PlayerInRangeConditionSequence : Composite
    {
        [SerializeReference] public BlackboardVariable<bool> TargetInrange;

        [FormerlySerializedAs("self")] [SerializeReference]
        public BlackboardVariable<GameObject> Self;

        [FormerlySerializedAs("Target")] [SerializeReference]
        public BlackboardVariable<GameObject> target;


        public SphereCollider rangeCollider;

        protected override Status OnStart()
        {

            if (Self == null || Self.Value == null)
            {
                Debug.LogError("Enemy GameObject is not assigned in blackboard!");
                return Status.Failure;
            }

            rangeCollider = Self.Value.GetComponentInChildren<SphereCollider>();

            if (rangeCollider == null)
            {
                Debug.LogError("No Range");
                return Status.Failure;
            }

            return Status.Running;
        }

        protected override Status OnUpdate()
        {
            if (target == null || target.Value == null)
                return Status.Failure;

            if (rangeCollider == null)
            {
                Debug.LogWarning("Range Collider not set");
                return Status.Failure;
            }
            
            Vector3 worldCenter = Self.Value.transform.TransformPoint(rangeCollider.center);
            float scale = Mathf.Max(
                Self.Value.transform.lossyScale.x,
                Self.Value.transform.lossyScale.y,
                Self.Value.transform.lossyScale.z
            );
            
            float effectiveRange = rangeCollider.radius * Self.Value.transform.lossyScale.x;
            float distance = Vector3.Distance(worldCenter, target.Value.transform.position);
            bool isInDistance = distance <= effectiveRange;

            
            Debug.Log($"[PlayerInRangeCondition] Distance to target: {distance}, In range: {isInDistance}");

            if (TargetInrange != null)
            {
                TargetInrange.Value = isInDistance;
            }


            return isInDistance ? Status.Success : Status.Failure;
        }

        protected override void OnEnd()
        {
        // Clean up if needed
        }
    }
}


