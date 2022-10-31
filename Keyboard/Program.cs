using System;
using System.Collections.Generic;
using Keyboard.Keyboards;
using Keyboard.Managers;

namespace Keyboard
{
    class Program
    {
        static void Main(string[] args)
        {
            var teamMemberNames = new[]
            {
                "John Smith",
                "Jane Doe",
            };
            
            var manager = new KeyboardManager(new QwertyKeyboard());

            var totalLengthLookup = new Dictionary<string, double>();
            var averageJumpLookup = new SortedDictionary<double, string>();

            foreach (var teamMemberName in teamMemberNames)
            {
                var nameDistance = manager.GetWordLength(teamMemberName);
                var jumpAverageDistance = nameDistance / (teamMemberName.Length - 1);
                totalLengthLookup.Add(teamMemberName, nameDistance);
                averageJumpLookup.Add(jumpAverageDistance, teamMemberName);
            }
            
            foreach (var entry in averageJumpLookup)
            {
                Console.WriteLine("{0,-18}{1,10}{2,10}", entry.Value, $"{Math.Round(totalLengthLookup[entry.Value], 2)}mm", $"{Math.Round(entry.Key, 2)}mm");
            }
        }
    }
}