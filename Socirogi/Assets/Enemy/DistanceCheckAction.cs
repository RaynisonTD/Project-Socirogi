using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "DistanceCheck", story: "Check [inRange]", category: "Action", id: "54c07ac275f3d85b66c3053d3e37db68")]
public partial class DistanceCheckAction : Action
{
    [SerializeReference] public BlackboardVariable<bool> InRange;

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

