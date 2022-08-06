using UnityEngine;

public abstract class PlayerBaseState
{
    private PlayerBaseState superState;

    public void SetSuperState(PlayerBaseState superState)
    {
        this.superState = superState;
    }

    public virtual void EnterState(PlayerStateManager player)
    {
        superState?.EnterState(player);
    }

    public virtual void UpdateState(PlayerStateManager player)
    {
        superState?.UpdateState(player);
        
    }

    public virtual void OnTriggerEnter(PlayerStateManager player, Collider collider)
    {
        superState?.OnTriggerEnter(player, collider);
    }
    
    public virtual void OnTriggerStay(PlayerStateManager player, Collider collider)
    {
        superState?.OnTriggerStay(player, collider);
    }

    public virtual void OnTriggerExit(PlayerStateManager player, Collider collider)
    {
        superState?.OnTriggerExit(player, collider);
    }
}
