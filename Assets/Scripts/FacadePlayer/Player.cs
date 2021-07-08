using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private MobileInput _input;
    private PlayerFacade _facade;
    private Rigidbody _rigidbody;
    private Camera _camera;

    public void Init(MobileInput input, PlayerFacade facade)
    {
        _input = input;
        _facade = facade;
        _input.PlayerActions.OnAttacked += Attack;
        _input.PlayerActions.OnJumped += Jump;
        _input.PlayerActions.OnInteracted += Interact;
        _input.PlayerActions.OnCasted += Cast;
    }

    private void Awake()
    {
        _camera = Camera.main;
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        MoveCamera();
        Move();
    }

    private void Attack()
    {
        _facade.PerformAttack();
    }

    private void Move()
    {
        var speed = _rigidbody.velocity.magnitude;
        _facade.PerformMovement(speed);
    }

    private void Jump()
    {
        _facade.PerformJump();
    }

    private void Interact()
    {
        _facade.PerformInteraction();
    }

    private void Cast()
    {
        _facade.PerformCast();
    }

    private void MoveCamera()
    {
        var input = _input.CameraLook.ReadCameraInput();
        _facade.PerformCameraRotation(_camera, input, transform);
    }
    
}