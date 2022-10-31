using Keyboard.Keyboards;

namespace Keyboard.Managers
{
    public class KeyboardManager
    {
        private readonly IKeyboard _keyboard;

        public KeyboardManager(IKeyboard keyboard)
        {
            _keyboard = keyboard;
        }
        
        public double GetWordLength(string word)
        {
            word = word.ToLower().Replace(" ", "");
            
            var lengthSoFar = 0.0;
            for (var i = 0; i < word.Length-1; i++)
            {
                var currentChar = word[i];
                var nextChar = word[i + 1];
                lengthSoFar += _keyboard.GetKeySeparationDistance(currentChar, nextChar);
            }

            return lengthSoFar;
        }
    }
}