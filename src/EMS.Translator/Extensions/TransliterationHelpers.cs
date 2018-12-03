using UnidecodeSharpCore;

namespace EMS.Translator.Extensions
{
    public static class Transliteration
    {
        public static string Transliterate(this string src) => src.Unidecode();
    }
}