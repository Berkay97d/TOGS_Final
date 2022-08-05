using UnityEngine;

public class AppleChewedState : AppleBaseState
{
    private float destroyCountdown = 5.0f;
    public override void EnterState(AppleStateManager apple)
    {
        Debug.Log("hello from the apple chewed state");
        
        apple.transform.GetChild(1).GetComponent<Renderer>().material = apple.chewedMat;

        apple.gameObject.AddComponent<Rigidbody>();
        apple.GetComponent<Rigidbody>().mass = 3;
    }

    public override void UpdateState(AppleStateManager apple)
    {
        if (destroyCountdown > 0)
        {
            destroyCountdown -= Time.deltaTime;
        }
        else
        {
            Object.Destroy(apple.gameObject);
        }
    }

    public override void OnCollisionEnter(AppleStateManager apple, Collision collision)
    {
        
    }
}
