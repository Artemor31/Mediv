public class MobileInput : InputSystem
{
    public override IMoveInput MoveInput { get; protected set; }
    public override ICameraInput CameraInput { get; protected set; }
}