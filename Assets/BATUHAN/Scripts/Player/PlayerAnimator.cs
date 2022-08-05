using NaughtyAttributes;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    
    public Animator animator0;
    [AnimatorParam("animator0")]
    
    public int running, harvesting, planting;

    public void IdleAnimation()
    {
        animator0.SetBool(running, false);
        animator0.SetBool(harvesting, false);
        animator0.SetBool(planting, false);
    }

    public void RunAnimation()
    {
        animator0.SetBool(running, true);
        animator0.SetBool(harvesting, false);
        animator0.SetBool(planting, false);
    }
    
    public void HarvestAnimation()
    {
        animator0.SetBool(running, false);
        animator0.SetBool(harvesting, true);
        animator0.SetBool(planting, false);
    }

    public void PlantAnimation()
    {
        animator0.SetBool(running, false);
        animator0.SetBool(harvesting, false);
        animator0.SetBool(planting, true);
    }
}
