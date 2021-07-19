using UnityEngine;

public class Mover 
{
    private const float MaxSpeed = 5.8f;
    
    private readonly InputSystem _inputSystem;
    private readonly StateResolve _resolve;
    private readonly PlayerCamera _camera;
    private float _jumpForce = 5f;

    public Mover(InputSystem inputSystem, StateResolve resolve, PlayerCamera camera)
    {
        _inputSystem = inputSystem;
        _resolve = resolve;
        _camera = camera;
    }

    public void Move(Rigidbody rigidbody, Transform transform)
    {
        if (_resolve.CanWalk == false) return;
        
        var input = GetInput();
        
        MoveBody(rigidbody, input);
        RotateBody(input, transform);
    }  
    
    private Vector3 GetInput()
    {
        var inputValue = _inputSystem.MoveInput.GetInput();
        var input = new Vector3(inputValue.x, 0, inputValue.y);
        return input;
    }

    private void MoveBody(Rigidbody rigidbody, Vector3 input)
    {
        var velocity = Quaternion.AngleAxis(_camera.CameraAngleY, Vector3.up) * input * MaxSpeed;
        rigidbody.velocity = new Vector3(velocity.x, rigidbody.velocity.y, velocity.z);
    }

    private void RotateBody(Vector3 input, Transform transform)
    {
        if (!(Mathf.Abs(input.x) >= 0.01f) && !(Mathf.Abs(input.y) >= 0.01f)) return;
        
        var forward = input.normalized + Vector3.forward * 0.0001f;
        var signedAngle = Vector3.SignedAngle(Vector3.forward, forward, Vector3.up);
        var angle = _camera.CameraAngleY + signedAngle;
        
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
    }
}