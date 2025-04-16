using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace Enemy
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Moving", story: "[Agent] plays [animation]", category: "Action/Animation", id: "2123caf5c80697f9d804e5f70cec0027")]
    public partial class MovingAction : Action
    {
        [SerializeReference] public BlackboardVariable<GameObject> Agent;
        [SerializeReference] public BlackboardVariable<Animator> Animation;

        protected override Status OnStart()
        {
            return Status.Running;
        }

        protected override Status OnUpdate()
        {
        
            return Status.Success;
        }

        protected override void OnEnd()
        {
        }
    }
}

