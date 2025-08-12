using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager<T> where T : IStateBehavior
{
    private readonly Dictionary<Type, IStateBehavior> _stateBehaviorDict = new();

    public void AddState(Type stateType, T state)
    {
        if (state.GetType() != stateType)
        {
            throw new Exception($"Added state is not {stateType}");
        }
        if (!_stateBehaviorDict.ContainsKey(stateType))
        {
            _stateBehaviorDict.Add(stateType, state);
        }
    }
    public void RemoveState(Type stateType)
    {
        if (_stateBehaviorDict.ContainsKey(stateType))
        {
            _stateBehaviorDict.Remove(stateType);
        }
        else
        {
            throw new Exception($"Removed state is not in _stateBehaviorDict");
        }
    }
    public T GetState(Type stateType)
    {
        if (_stateBehaviorDict.ContainsKey(stateType))
        {
            return (T)_stateBehaviorDict[stateType];
        }
        else
        {
            throw new Exception($"{stateType} is not in _stateBehaviorDict");
        }
    }
}
