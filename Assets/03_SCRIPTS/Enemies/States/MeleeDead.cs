using UnityEngine;
using XephTools;

public class MeleeDead : IState<EnemyMelee>
{
    float _timer = 0f;

    public void Enter(EnemyMelee agent)
    {
        agent.SetVelocity(Vector3.zero);
        _timer = 0f;
    }

    public void Update(EnemyMelee agent, float deltaTime)
    {
        if (_timer >= agent.idleTime)
        {
            _timer = 0;
            agent.PlayDeathLine();
            return;
        }
        _timer += deltaTime;
    }

    public void Exit(EnemyMelee agent)
    {

    }
}

public class MeleeXtraDead : IState<EnemyMelee>
{
    public void Enter(EnemyMelee agent)
    {
        agent.OnXtraDead();
    }
    public void Update(EnemyMelee agent, float deltaTime) { }
    public void Exit(EnemyMelee agent) { }
}
