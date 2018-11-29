using System;
using System.Collections;
using System.IO;
using System.Net.Http;
using System.Text;
using EMS.ConfigurationProviders.Tests.Models;
using Newtonsoft.Json;
using NUnit.Framework;
using NSubstitute;

namespace EMS.ConfigurationProviders.Tests.WebApi
{
    [TestFixture]
    public class WebApiConfigurationProviderTests
    {
        private HttpClient _httpClient;

        [SetUp]
        public void SetUp()
        {
            var expectedResult = new SampleConfiguration
            {
                Name = "Sample",
                Nested = new NestedConfiguration {Name = "Nested"}
            };

            var serializer = new JsonSerializer();

            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);

            serializer.Serialize(new JsonTextWriter(writer), expectedResult);

            _httpClient = Substitute.For<HttpClient>();
            _httpClient.GetStreamAsync(Arg.Any<Uri>())
                .Returns(stream);
        }
    }
}