
public class MobileInput : InputSystem
{
    public sealed override IMoveInput MoveInput { get; protected set; }
    public sealed override ICameraLook CameraLook { get; protected set; }
    
    public MobileActions MobileActions { get; private set; }
    
    public MobileInput()
    {
        MoveInput = new MobileMoveInput();
        CameraLook = new MobileCamera();
    }
}