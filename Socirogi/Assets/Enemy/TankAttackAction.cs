using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "TankAttack", story: "[Self] Hits [Target]", category: "Action", id: "6357188568c47f206f64782da99d270e")]
public partial class TankAttackAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Target;

    public void Hit()
    {
        Debug.Log("Hit ");
    }

 }


