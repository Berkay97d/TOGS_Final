using NaughtyAttributes;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector] public Rigidbody rigidbody;

    public float horizontalMovementSpeed, verticalMovementSpeed;

    public FloatingJoystick _floatingJoystick;
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
}
