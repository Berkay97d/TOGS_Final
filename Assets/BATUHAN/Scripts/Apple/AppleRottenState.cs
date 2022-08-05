using DG.Tweening;
using UnityEngine;

public class AppleRottenState : AppleBaseState
{
    public override void EnterState(AppleStateManager apple)
    {
        Debug.Log("hello from the apple rotten state");
        apple.transform.GetChild(1).GetComponent<Renderer>().material = apple.rottenMat;
        
        apple.gameObject.AddComponent<Rigidbody>();
        apple.GetComponent<Rigidbody>().mass = 3;
        
        apple.transform.DOScaleY(0.1f, 0.5f);
    }

    public override void UpdateState(AppleStateManager apple)
    {
    }

    public override void OnCollisionEnter(AppleStateManager apple, Collision collision)
    {
        
    }
}
