public abstract class MovementState
{
    protected MovementStateMachine context;

    public MovementState(MovementStateMachine context)
    {
        this.context = context;
    }
    public abstract void Update();
    public virtual void OnExit() { }
    public virtual void OnEnter() { }

}
