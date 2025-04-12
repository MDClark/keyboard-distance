using System;
using System.Linq;
using Keyboard.Enums;

namespace Keyboard.Keyboards;

public abstract class KeyboardBase : IKeyboard
{
    private const decimal KeyCapSeparation = 19.05M;

    public abstract char[][] Layout { get; }

    public IndexCoordinate GetKeyIndexCoordinate(char character)
    {
        for (var row = 0; row < Layout.Length; row++)
        {
            if (Layout[row].Contains(character))
            {
                var column = Array.FindIndex(Layout[row], r => r == character);
                return new IndexCoordinate(row, column);
            }
        }

        throw new ArgumentException($"Character '{character}' not found.");
    }

    public Coordinate GetKeyCoordinate(char character)
    {
        var coordinatesTo = GetKeyIndexCoordinate(character);

        var verticalSeparationIndex = Math.Abs(0 - coordinatesTo.Row);
        var horizontalSeparationIndex = Math.Abs(0 - coordinatesTo.Column);

        var horizontalComponent = horizontalSeparationIndex * KeyCapSeparation;
            
        if (verticalSeparationIndex == 0)
        {
            return new Coordinate((double)horizontalComponent + ((double)KeyCapSeparation / 2), 0);
        }
            
        var verticalComponent = verticalSeparationIndex * KeyCapSeparation;
        var staggerComponent = GetHorizontalStaggerDistance(new IndexCoordinate(0, 0), coordinatesTo);
            
        var horizontalSum = decimal.Add(horizontalComponent, staggerComponent);
        var verticalSquared = decimal.Multiply(verticalComponent, verticalComponent);
        var horizontalComponentSquared = decimal.Multiply(horizontalSum, horizontalSum);
        var allComponentsSummed = decimal.Add(verticalSquared, horizontalComponentSquared);

        throw new NotImplementedException();
    }

    public double GetKeySeparationDistance(char from, char to)
    {
        if (from == to)
        {
            return 0;
        }
            
        var coordinatesFrom = GetKeyIndexCoordinate(from);
        var coordinatesTo = GetKeyIndexCoordinate(to);

        var verticalSeparationIndex = Math.Abs(coordinatesFrom.Row - coordinatesTo.Row);
        var horizontalSeparationIndex = Math.Abs(coordinatesFrom.Column - coordinatesTo.Column);

        var horizontalComponent = horizontalSeparationIndex * KeyCapSeparation;
            
        if (verticalSeparationIndex == 0)
        {
            return (double)horizontalComponent;
        }
            
        var verticalComponent = verticalSeparationIndex * KeyCapSeparation;
        var staggerComponent = GetHorizontalStaggerDistance(coordinatesFrom, coordinatesTo);
            
        var horizontalSum = decimal.Add(horizontalComponent, staggerComponent);
        var verticalSquared = decimal.Multiply(verticalComponent, verticalComponent);
        var horizontalComponentSquared = decimal.Multiply(horizontalSum, horizontalSum);
        var allComponentsSummed = decimal.Add(verticalSquared, horizontalComponentSquared);
        return Math.Sqrt((double)allComponentsSummed);
    }

    private decimal GetHorizontalStaggerDistance(IndexCoordinate from, IndexCoordinate to)
    {
        decimal staggerComponent;

        // If 0 to 1 (or vice-versa), 1/4 key cap separation
        // If 1 to 2 (or vice-versa), 1/2 key cap separation
        // If 0 to 2 (or vice-versa), 3/4 key cap separation
        if (from.Row != 2 && to.Row != 2) // 0 and 1 - 1/4
        {
            staggerComponent = decimal.Multiply(KeyCapSeparation, 0.25M);
        }
        else if (from.Row != 0 && to.Row != 0) // 1 and 2 - 1/2
        {
            staggerComponent = decimal.Multiply(KeyCapSeparation, 0.5M);
        }
        else // 0 to 2 - 3/4
        {
            staggerComponent = decimal.Multiply(KeyCapSeparation, 0.75M);
        }

        var direction = GetKeyJumpDirection(from, to);

        return direction.HasFlag(Direction.DownAndRight) || direction.HasFlag(Direction.UpAndLeft) ? staggerComponent : -staggerComponent;
    }

    private Direction GetKeyJumpDirection(IndexCoordinate from, IndexCoordinate to)
    {
        var direction = Direction.None;
        direction |= from.Row < to.Row ? Direction.Down : Direction.Up;
        direction |= from.Column <= to.Column ? Direction.Right : Direction.Left;
        return direction;
    }
}