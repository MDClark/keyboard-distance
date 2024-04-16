namespace Keyboard.Keyboards;

public class QwertyKeyboard : KeyboardBase
{
    public override char[][] Layout { get; } =
    {
        new[] { 'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p' },
        new[] { 'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l' },
        new[] { 'z', 'x', 'c', 'v', 'b', 'n', 'm' }
    };
}