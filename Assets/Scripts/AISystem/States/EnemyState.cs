// Base class for all states
public abstract class EnemyState
{
    protected EnemyAI enemy;
    
    public EnemyState(EnemyAI enemy)
    {
        this.enemy = enemy;
    }

    // Used to make some setup / initialization when entering the state
    public virtual void Enter() { }
    
    // Used for updating the state each frame
    public virtual void Tick() { }
    
    // Used to deactivate / de-initialize the state when changing to another
    public virtual void Exit() { }
}