namespace Keyboard.Keyboards
{
    public interface IKeyboard
    {
        char[][] Layout { get; }

        IndexCoordinate GetKeyIndex(char character);

        double GetKeySeparationDistance(char from, char to);
    }
}