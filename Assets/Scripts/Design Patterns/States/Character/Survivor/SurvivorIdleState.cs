using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivorIdleState : SurvivorState
{
    public SurvivorIdleState(Survivor survivor) : base(survivor)
    {

    }
    public override void EnterState()
    {
        //_survivor.PlayAnimation(SurvivorAnimmation.Idle);
    }

    public override void ExitState()
    {

    }

    public override void UpdateState()
    {
        if (_survivor.GetMovement() != Vector2.zero)
        {
            //_survivor.SwitchState(_survivor.GetState<SurvivorWalkState>());
        }
    }
}
