using Keyboard.Keyboards;
using Keyboard.Managers;
using NUnit.Framework;

namespace Keyboard.UnitTests
{
    [TestFixture]
    public class KeyboardManagerTests
    {
        private KeyboardManager _keyboardManager;

        [SetUp]
        public void Setup()
        {
            _keyboardManager = new KeyboardManager(new QwertyKeyboard());
        }

        [TestCase("minimum",248.0)]
        [TestCase("dichlorodiphenyltrichloroethane",2119.5)]
        [TestCase("antidisestablishmentarianism",2031.8)]
        [TestCase("superincomprehensibleness",1665.5)]
        [TestCase("palapala",1112.9)]
        [TestCase("papal",655.7)]
        [TestCase("papa",503.3)]
        [TestCase("as",19.1)]
        [TestCase("ass",19.1)]
        [TestCase("poo",19.1)]
        [TestCase("poop",38.1)]
        [TestCase("deess",43.4)]
        [TestCase("weeds",57.7)]
        [TestCase("weewee",57.2)]
        [TestCase("weeweed",76.8)]
        [TestCase("dismantlement",1124.2)]
        [TestCase("spa",316.6)]
        [TestCase("fuzz",162.8)]
        [TestCase("gazogene",523.5)]
        [TestCase("schizogony",638.2)]
        [TestCase("wayzgoose",502.8)]
        [TestCase("outreach",255.3)]
        [TestCase("ethmoids",278.4)]
        [TestCase("nonmischievousness",965.9)]
        [TestCase("nonsubstitutionary",989.4)]
        [TestCase("wee wee",57.2)]
        [TestCase("qwertyuiop",171.45)]
        public void Manager_GetLength(string word, double expectedLength)
        {
            var actualLength = _keyboardManager.GetWordLength(word);
            Assert.AreEqual(expectedLength, actualLength, 0.1);
        }
    }
}