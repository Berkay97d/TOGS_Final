using UnityEngine;
using DG.Tweening;

public class AppleGrowingState : AppleBaseState
{
    public override void EnterState(AppleStateManager apple)
    {
        Debug.Log("hello from the apple growing state");

        apple.transform.localScale = Vector3.one * 0.1f;
    }

    public override void UpdateState(AppleStateManager apple)
    {
        apple.transform.DOScale(Vector3.one, 1f)
            .SetDelay(1f)
            .OnComplete(() =>
            {
                apple.SwitchState(apple.wholeState);
            });
    }

    public override void OnCollisionEnter(AppleStateManager apple, Collision collision)
    {
        
    }
}
