using UnityEngine;
using XephTools;

public class MeleeIdle : IState<EnemyMelee>
{
    float _timer = 0f;

    public void Enter (EnemyMelee agent)
    {
        _timer = 0f;
        agent.SetVelocity(Vector3.zero);
    }

    public void Update(EnemyMelee agent, float deltaTime)
    {
        if (agent.doesPatrol)
        {
            if (_timer >= agent.idleTime)
            {
                agent.ChangeState(EnemyState.Patrol);
                return;
            }
            _timer += deltaTime;
        }

        if (agent.isPlayerDetected)
        {
            agent.ChangeState(EnemyState.Attack);
            return;
        }
    }

    public void Exit (EnemyMelee agent)
    {

    }
}
