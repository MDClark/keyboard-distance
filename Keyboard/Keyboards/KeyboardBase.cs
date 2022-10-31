using System;
using System.Linq;

namespace Keyboard.Keyboards
{
    public abstract class KeyboardBase : IKeyboard
    {
        public const decimal KeyCapSeparation = 19.05M;
        
        public abstract char[][] Layout { get; }

        public IndexCoordinate GetKeyIndex(char character)
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

        public double GetKeySeparationDistance(char from, char to)
        {
            if (from == to)
            {
                return 0;
            }
            
            var coordinatesFrom = GetKeyIndex(from);
            var coordinatesTo = GetKeyIndex(to);

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

            return from.Row < to.Row != from.Column <= to.Column ? -staggerComponent : staggerComponent;
        }
    }
}