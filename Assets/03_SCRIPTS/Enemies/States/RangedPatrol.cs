﻿using UnityEngine;
using XephTools;

public class RangedPatrol : IState<EnemyRanged>
{

    public void Enter(EnemyRanged agent)
    {

    }

    public void Update(EnemyRanged agent, float deltaTime)
    {
        Vector3 target = agent.moveTarget;
        if ((target - agent.transform.position).sqrMagnitude <= agent.patrolArriveDistance * agent.patrolArriveDistance)
        {
            agent.NextPatrolPoint();
            agent.ChangeState(EnemyState.Idle);
            return;
        }
        if (agent.isPlayerDetected)
        {
            agent.ChangeState(EnemyState.Attack);
            return;
        }

        Vector3 velocity = (target - agent.transform.position);
        velocity.y = 0f;
        velocity = velocity.normalized * agent.patrolSpeed;
        agent.SetVelocity(velocity);
        agent.transform.LookAt(agent.transform.position + velocity);
    }

    public void Exit(EnemyRanged agent)
    {
    }
}