using UnityEngine;
using XephTools;

public class RangedAttack : IState<EnemyRanged>
{
    float _timer = 0f;
    public void Enter(EnemyRanged agent)
    {
        _timer = 0f;
    }

    public void Update(EnemyRanged agent, float deltaTime)
    {
        agent.transform.LookAt(new Vector3(agent.playerPosition.x, agent.transform.position.y, agent.playerPosition.z));

        if (_timer >= agent.attackRate)
        {
            agent.Attack();
            _timer = 0f;
            return;
        }

        _timer += deltaTime;
    }

    public void Exit(EnemyRanged agent)
    {
    }
}