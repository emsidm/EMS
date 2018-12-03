using EMS.Translator.Extensions;
using NUnit.Framework;

namespace EMS.Translator.Tests
{
    [TestFixture]
    public class TransliterationTests
    {
        [Test]
        public void Transliteration_ReturnsLatin()
        {
            string src = "Работа с кириллицей";
            string expected = "Rabota s kirillitsey";
            
            Assert.That(src.Transliterate(), Is.EqualTo(expected));
        }
    }
}