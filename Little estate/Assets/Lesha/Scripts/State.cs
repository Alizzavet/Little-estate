public abstract class State
{
    protected BatEnemy batEnemy;
    public abstract void Enter();
    public abstract void Execute();
    public abstract void Exit();
}