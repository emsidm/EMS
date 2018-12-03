using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace EMS.Translator.Extensions
{
    public static class ConventionHelpers
    {
        public static string ReplaceByConvention(this string template,
            IDictionary<string, string> replacements)
        {
            return Regex.Replace(template, @"{(?<exp>[^}]+)}", match =>
            {
                var p = replacements.Select(pair => Expression.Parameter(pair.Value.GetType(), pair.Key));
                var e = DynamicExpressionParser.ParseLambda(p.ToArray(), null, match.Groups["exp"].Value,
                    replacements.Values.ToArray());
                return (e.Compile().DynamicInvoke(replacements.Values.ToArray()) ?? "").ToString();
            });
        }
    }
}