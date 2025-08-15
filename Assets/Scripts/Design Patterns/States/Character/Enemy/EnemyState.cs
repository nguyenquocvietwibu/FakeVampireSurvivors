using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState : IStateBehavior
{
    protected Enemy _enemy;

    public EnemyState(Enemy enemy)
    {
        _enemy = enemy;
    }

    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void UpdateState();
}
