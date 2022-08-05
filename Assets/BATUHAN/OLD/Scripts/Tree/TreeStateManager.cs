using UnityEngine;

public class TreeStateManager : MonoBehaviour
{
    TreeBaseState currentState;
    
    public TreeIdleState idleState = new TreeIdleState();
    public TreeDeforestState deforestState = new TreeDeforestState();

    private void Start()
    {
        currentState = idleState;
        currentState.EnterState(this);
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(TreeBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
    
    
    public void TakeDamage(int damage)
    {
        currentState.TakeDamage(this, damage);
    }
}
