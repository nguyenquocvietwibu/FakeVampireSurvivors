using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SurvivorWalkState : SurvivorState
{
    private Coroutine _walkRoutine;
    public SurvivorWalkState(Survivor survivor) : base(survivor)
    {

    }
    public override void EnterState()
    {
        //_survivor.PlayAnimation(SurvivorAnimmation.Walk);
        _walkRoutine = _survivor.StartCoroutine(WalkProcess());
    }

    public override void ExitState()
    {
        if (_walkRoutine != null)
        {
            _survivor.StopCoroutine(_walkRoutine);
        }
    }

    public override void UpdateState()
    {
        if (_survivor.GetVelocity() == Vector2.zero)
        {
            //_survivor.SwitchState(_survivor.GetState<SurvivorIdleState>());
        }
        _survivor.CheckFlipXSprite();
    }

    private IEnumerator WalkProcess()
    {
        while (true)
        {
            if (_survivor.TryGetStatKey(Stat.MoveSpeed, out Stat moveSpeedKey))
            {
                if (_survivor.TryGetStatValue(moveSpeedKey, out float moveSpeedValue))
                {
                    _survivor.Move(_survivor.GetMovement() * moveSpeedValue);
                }
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
