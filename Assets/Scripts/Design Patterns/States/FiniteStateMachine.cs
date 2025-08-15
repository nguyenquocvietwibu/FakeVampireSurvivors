using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine
{
    private IStateBehavior _currentState;

    public IStateBehavior currentState => _currentState;

    public FiniteStateMachine(IStateBehavior initialedState)
    {
        InitializeState(initialedState);
    }

    public void SwitchState(IStateBehavior switchedState)
    {
        if (_currentState != switchedState)
        {
            _currentState.ExitState();
            _currentState = switchedState;
            currentState.EnterState();
        }
    }
    private void InitializeState(IStateBehavior initialedState)
    {
        _currentState = initialedState;
        _currentState.EnterState();
    }

    public void UpdateState()
    {
        _currentState.UpdateState();
    }
}
