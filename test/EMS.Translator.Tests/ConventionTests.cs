using System.Collections.Generic;
using System.Linq.Dynamic.Core.Exceptions;
using EMS.Translator.Extensions;
using NUnit.Framework;

namespace EMS.Translator.Tests
{
    [TestFixture]
    public class ConventionTests
    {
        [Test]
        public void SimpleSubstitution_ReplacesValue()
        {
            var substitutions = new Dictionary<string, string>
            {
                {"firstName", "Pesho"},
                {"lastName", "Gosho"}
            };

            Assert.AreEqual("Pesho Gosho", "{firstName} {lastName}".ReplaceByConvention(substitutions));
        }

        [Test]
        public void MethodsInSubstitutions_Invoked()
        {
            var substitutions = new Dictionary<string, string>
            {
                {"firstName", "Pesho"},
                {"lastName", "Gosho"}
            };

            Assert.AreEqual("P. Gosho", "{firstName.Substring(0,1)}. {lastName}".ReplaceByConvention(substitutions));
        }

        [Test]
        public void NoSubstitute_Throws()
        {
            var substitutions = new Dictionary<string, string>
            {
                {"firstName", "Pesho"}
            };

            Assert.Throws<ParseException>(() =>
                "{firstName.Substring(0,1)}. {lastName}".ReplaceByConvention(substitutions));
        }
    }
}