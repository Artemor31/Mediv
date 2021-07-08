using System;

[Serializable]
public class MobileInput : InputSystem
{
    public sealed override IMoveInput MoveInput { get; protected set; }
    public sealed override ICameraLook CameraLook { get; protected set; }
    
    public IPlayerActions PlayerActions { get; private set; }
    
    public MobileInput(IMoveInput moveInput, ICameraLook mobileCamera, IPlayerActions actions)
    {
        MoveInput = moveInput;
        CameraLook = mobileCamera;
        PlayerActions = actions;
    }
}