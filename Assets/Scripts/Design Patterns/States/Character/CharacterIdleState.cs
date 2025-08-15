using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterIdleState : CharacterState
{
    public CharacterIdleState(Character character) : base(character)
    {

    }

    public override void EnterState()
    {
        _character.PlayAnimation(CharacterAnimationHashManager.idle);
    }

    public override void ExitState()
    {

    }

    public override void UpdateState()
    {
        if (_character.GetNormalizedMovement() != Vector2.zero && _character.CanMove)
        {
            //if (_character.GetState<CharacterWalkState>(out CharacterState idleState))
            //{
            //    _character.SwitchState(idleState);
            //}

        }
    }
}
