using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character, IDamageable
{
    protected override void Awake()
    {
        base.Awake();
        _fsm = new FiniteStateMachine(GetState<CharacterWalkState>());
    }
    public void Damage(float damage)
    {
        throw new System.NotImplementedException();
    }
}
