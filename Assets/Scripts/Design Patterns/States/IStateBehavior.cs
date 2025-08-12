using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStateBehavior
{
    public void EnterState();
    public void UpdateState();
    public void ExitState();
}
