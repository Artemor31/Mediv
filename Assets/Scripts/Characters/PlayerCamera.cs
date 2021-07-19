using UnityEngine;

public class PlayerCamera
{
    private const float CameraAngleSpeed = 0.1f;
    private const float CameraPosSpeed = 0.02f;
    
    public float CameraAngleY;
    
    private readonly InputSystem _inputSystem;
    private readonly Camera _camera;
    
    private float _cameraPosY = 3f;
    private float _x = 2;
    private float _z = 2;
    
    
    public PlayerCamera(InputSystem inputSystem, Camera camera)
    {
        _inputSystem = inputSystem;
        _camera = camera;
    }

    public void UpdatePosition(Vector3 position)
    {
        var input = _inputSystem.CameraInput.GetInput();
        CameraAngleY += input.x * CameraAngleSpeed;
        _cameraPosY = Mathf.Clamp(_cameraPosY - input.y * CameraPosSpeed, 0, 5f);
        
        Transform cameraPosition;
        (cameraPosition = _camera.transform).position =
            position + Quaternion.AngleAxis(CameraAngleY - 220f, Vector3.up) 
            * new Vector3(_x, _cameraPosY, _z);
            
        _camera.transform.rotation = Quaternion.LookRotation(
            position + Vector3.up * 2f - cameraPosition.position, 
            Vector3.up);
    }  
}