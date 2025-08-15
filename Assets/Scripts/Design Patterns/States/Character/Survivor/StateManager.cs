using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StateManager<T> where T : IStateBehavior
{
    private readonly Dictionary<Type, IStateBehavior> _stateBehaviorDict = new();

    public void AddState(T state)
    {
        Type stateType = state.GetType();
        if (!_stateBehaviorDict.ContainsKey(stateType))
        {
            _stateBehaviorDict.Add(stateType, state);
        }
    }
    //public void RemoveState(Type stateType)
    //{
    //    if (_stateBehaviorDict.ContainsKey(stateType))
    //    {
    //        _stateBehaviorDict.Remove(stateType);
    //    }
    //}
    public T GetState(Type stateType)
    {
        return (T)_stateBehaviorDict[stateType];
    }
}
