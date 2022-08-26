using NaughtyAttributes;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector] public PlayerStateManager _playerStateManager;
    
    [HideInInspector] public Rigidbody rigidbody;

    public float horizontalMovementSpeed, verticalMovementSpeed;

    public FloatingJoystick _floatingJoystick;
    [Range(0.01f, 0.5f)][SerializeField] private float movementStartValue = 0.1f;
    
    
    public float HorizontalJoystickInput { get { return _floatingJoystick.Horizontal; } }
    public float VerticalJoystickInput { get { return _floatingJoystick.Vertical; } }
    
    public float HorizontalMovement
    {
        get
        {
            return 
                HorizontalJoystickInput 
                * horizontalMovementSpeed 
                * _playerStateManager.StateMovementSpeedEffect() 
                * _playerStateManager.SpeedMultiplier;
        }
    }

    public float VerticalMovement
    {
        get
        {
            return 
                VerticalJoystickInput 
                * verticalMovementSpeed 
                * _playerStateManager.StateMovementSpeedEffect() 
                * _playerStateManager.SpeedMultiplier;
        }
    }

    public float MagnitudeOfPlayerSpeed()
    {
        return new Vector2(HorizontalMovement, VerticalMovement).magnitude;
    }

    public float MagnitudeOfPlayerS()
    {
        return new Vector2(horizontalMovementSpeed/HorizontalMovement, verticalMovementSpeed/VerticalMovement).magnitude;
    }
    public float MagnitudeOfJoystickInput()
    {
        return new Vector2(HorizontalJoystickInput, VerticalJoystickInput).magnitude;
    }
    public bool IsEnoughJoystickInputForMovement()
    {
        if (MagnitudeOfJoystickInput() >= movementStartValue)
            return true;
        return false;
    }
    
    
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        _playerStateManager = GetComponent<PlayerStateManager>();
    }
    
}