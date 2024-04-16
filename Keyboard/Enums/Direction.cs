using System;

namespace Keyboard.Enums;

[Flags]
public enum Direction
{
    None = 0,
    Left = 1,
    Right = 2,
    Up = 4,
    Down = 8,
    UpAndLeft = 5,
    UpAndRight = 6,
    DownAndLeft = 9,
    DownAndRight = 10,
}