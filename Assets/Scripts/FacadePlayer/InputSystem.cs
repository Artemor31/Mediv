using System;
using System.Collections.Generic;

public abstract class InputSystem
{
    public abstract IMoveInput MoveInput { get; protected set; }
    public abstract ICameraLook CameraLook { get; protected set; }
}