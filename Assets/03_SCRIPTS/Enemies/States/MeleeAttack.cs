using UnityEngine;
using XephTools;

public class MeleeAttack : IState<EnemyMelee>
{
    float _timer = 0f;
    public void Enter(EnemyMelee agent)
    {
        agent.SetAttackTexture();
        _timer = 0f;
    }

    public void Update(EnemyMelee agent, float deltaTime)
    {
        Vector3 velocity = (agent.playerPosition - agent.transform.position);
        velocity.y = 0f;
        velocity = velocity.normalized * agent.seekSpeed;
        agent.SetVelocity(velocity);
        agent.transform.LookAt(new Vector3(agent.playerPosition.x, agent.transform.position.y, agent.playerPosition.z));

        if (_timer >= agent.attackRate)
        {
            agent.Attack();
            _timer = 0f;
            return;
        }

        _timer += deltaTime;
    }

    public void Exit(EnemyMelee agent)
    {
    }
}