using System;

public abstract class SurvivorState : IStateBehavior
{
    protected Survivor _survivor;
    private 
    protected SurvivorState(Survivor survivor)
    {
        _survivor = survivor;
    }

    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void UpdateState();
}
