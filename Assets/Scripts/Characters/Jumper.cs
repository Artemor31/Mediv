using UnityEngine;

public class Jumper
{
    private const float JumpForce = 4f;

    private readonly Rigidbody _rigidbody;
    private readonly Transform _transform;
    
    private bool _onGround;

    public Jumper(Rigidbody rigidbody, Transform transform)
    {
        _transform = transform;
        _rigidbody = rigidbody;
    }
    
    public void Jump()
    {
        OnGroundCheck();
        if (!_onGround) return;
        
        var velocity = _rigidbody.velocity;
        velocity = new Vector3(velocity.x, JumpForce, velocity.z);
        _rigidbody.velocity = velocity;
        
        
        //_animator.Jump();
    }

    private void OnGroundCheck()
    {
        _onGround = Physics.Raycast(
            _transform.position + new Vector3(0, 0.05f, 0), 
            Vector3.down, 
            0.1f);
            
        //_animator.OnGround(_onGround);
    }
}