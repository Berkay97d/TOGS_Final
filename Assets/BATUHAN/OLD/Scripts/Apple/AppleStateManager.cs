using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleStateManager : MonoBehaviour
{
    public Material wholeMat, chewedMat, rottenMat;
    
    AppleBaseState currentState;

    public AppleGrowingState growingState = new AppleGrowingState();
    public AppleChewedState chewedState = new AppleChewedState();
    public AppleRottenState rottenState = new AppleRottenState();
    public AppleWholeState wholeState = new AppleWholeState();
    
    private void Start()
    {
        currentState = growingState;
        currentState.EnterState(this);
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(AppleBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollisionEnter(this, collision);
    }
}
