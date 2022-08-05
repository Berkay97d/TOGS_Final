using DG.Tweening;
using UnityEngine;

public class AppleWholeState : AppleBaseState
{
    private float durationCountdown = 10.0f;
    
    public override void EnterState(AppleStateManager apple)
    {
        Debug.Log("hello from the apple whole state");

        apple.transform.GetChild(1).GetComponent<Renderer>().material = apple.wholeMat;

        apple.transform.DOLocalMoveY(apple.transform.localPosition.y, 0.5f)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Yoyo);
       
    }

    public override void UpdateState(AppleStateManager apple)
    {
        if (durationCountdown > 0)
        {
            durationCountdown -= Time.deltaTime;
        }
        else
        {
            apple.transform.DOKill();
            apple.SwitchState(apple.rottenState);
        }
    }

    public override void OnCollisionEnter(AppleStateManager apple, Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            apple.transform.DOKill();
            apple.SwitchState(apple.chewedState);
        }
    }
}
