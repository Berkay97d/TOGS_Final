using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AppleBaseState
{
    public abstract void EnterState(AppleStateManager apple);

    public abstract void UpdateState(AppleStateManager apple);

    public abstract void OnCollisionEnter(AppleStateManager apple, Collision collision);
}
