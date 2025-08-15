using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterState : IStateBehavior
{
    protected Character _character;

    public CharacterState(Character character)
    {
        _character = character;
    }

    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void UpdateState();
}
