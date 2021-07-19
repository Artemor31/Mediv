public abstract class InputSystem
{
    public abstract IMoveInput MoveInput { get; protected set; }
    public abstract ICameraInput CameraInput { get; protected set; }
}