namespace Keyboard.Keyboards;

public interface IKeyboard
{
    char[][] Layout { get; }

    IndexCoordinate GetKeyIndexCoordinate(char character);
    Coordinate GetKeyCoordinate(char character);
    double GetKeySeparationDistance(char from, char to);
}