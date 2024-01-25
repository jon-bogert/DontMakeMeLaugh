using UnityEngine;
using XephTools;

public class RangedDead : IState<EnemyRanged>
{
    float _timer = 0f;

    public void Enter(EnemyRanged agent)
    {
        agent.SetVelocity(Vector3.zero);
        _timer = 0f;
    }

    public void Update(EnemyRanged agent, float deltaTime)
    {
        if (_timer >= agent.idleTime)
        {
            _timer = 0;
            agent.PlayDeathLine();
            return;
        }
        _timer += deltaTime;
    }

    public void Exit(EnemyRanged agent)
    {

    }
}

public class RangedXtraDead : IState<EnemyRanged>
{
    public void Enter(EnemyRanged agent)
    {
        agent.SanityBoost();
    }
    public void Update(EnemyRanged agent, float deltaTime) { }
    public void Exit(EnemyRanged agent) { }
}
