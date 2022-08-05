using UnityEngine;

public abstract class TreeBaseState
{
    public abstract void EnterState(TreeStateManager tree);

    public abstract void UpdateState(TreeStateManager tree);
    
    public abstract void TakeDamage(TreeStateManager tree, int damage);
}
