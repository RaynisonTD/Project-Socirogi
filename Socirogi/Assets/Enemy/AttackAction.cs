using System;
using Stats;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Attack", story: "[Enemy] Attack [Player]", category: "Action", id: "ff08fe5ec6a9797d441e6a58f1fcef96")]
public partial class AttackAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Enemy;
    [SerializeReference] public BlackboardVariable<GameObject> Player;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if(!Physics.Raycast(Enemy.Value.transform.position, Enemy.Value.transform.forward, out RaycastHit hit, 5f))
            return Status.Success;

        if (!hit.transform.TryGetComponent(out PlayerStatsComponent playerStats))
            return Status.Failure;
        
        playerStats.realTimeStats.health -= 5.0f;
        return Status.Success;
        
    }

    protected override void OnEnd()
    {
    }
}

