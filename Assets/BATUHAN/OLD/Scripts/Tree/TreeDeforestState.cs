using UnityEngine;

public class TreeDeforestState : TreeBaseState
{

    private float forestTime = 10;
    public override void EnterState(TreeStateManager tree)
    {
        Debug.Log("hello from the deforest state");
    }

    public override void UpdateState(TreeStateManager tree)
    {
        forestTime -= Time.deltaTime;
        if ( forestTime < 0 )
        {
            tree.SwitchState(tree.idleState);
        }
        // Debug.Log(forestTime);
    }

    public override void TakeDamage(TreeStateManager tree, int damage)
    {
    }
}
