using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWalkState : CharacterState
{
   
    private Coroutine _walkRoutine;
    public CharacterWalkState(Character character) : base(character)
    {

    }

    public override void EnterState()
    {
        _character.PlayAnimation(CharacterAnimationHashManager.walk);
        _walkRoutine = _character.StartCoroutine(WalkProcess());
    }

    public override void ExitState()
    {
        if (_walkRoutine != null)
        {
            _character.StopCoroutine(_walkRoutine);
        }
    }

    public override void UpdateState()
    {
        if (_character.GetVelocity() == Vector2.zero || !_character.CanMove)
        {
            //if (_character.GetState<CharacterIdleState>(out CharacterState idleState))
            //{
            //    _character.SwitchState(idleState);
            //}
           
        }
        _character.CheckFlipXSprite();
    }

    private IEnumerator WalkProcess()
    {
        while (true)
        {
            if (_character.TryGetStatKey(Stat.MoveSpeed, out Stat moveSpeedKey))
            {
                if (_character.TryGetStatValue(moveSpeedKey, out float moveSpeedValue))
                {
                    _character.Move(_character.GetNormalizedMovement() * moveSpeedValue);
                }
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
