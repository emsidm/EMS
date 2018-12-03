//using System;
//using System.Collections;
//using System.IO;
//using System.Net.Http;
//using System.Text;
//using System.Threading.Tasks;
//using EMS.ConfigurationProviders.Tests.Models;
//using EMS.ConfigurationProviders.WebApi;
//using Newtonsoft.Json;
//using NUnit.Framework;
//using NSubstitute;
//
//namespace EMS.ConfigurationProviders.Tests.WebApi
//{
//    [TestFixture]
//    public class WebApiConfigurationProviderTests
//    {
//        private HttpClient _httpClient;
//
//        [SetUp]
//        public void SetUp()
//        {
//            var expectedResult = new SampleConfiguration
//            {
//                Name = "Sample",
//                Nested = new NestedConfiguration {Name = "Nested"}
//            };
//
//            var serializer = new JsonSerializer();
//
//            var stream = new MemoryStream();
//            var writer = new StreamWriter(stream);
//
//            serializer.Serialize(new JsonTextWriter(writer), expectedResult);
//
//            _httpClient = Substitute.For<HttpClient>();
//            _httpClient.BaseAddress = new Uri("https://example.com");
//            _httpClient.GetStreamAsync(Arg.Any<Uri>()).Returns(stream);
//            _httpClient.GetStreamAsync(Arg.Any<string>()).Returns(stream);
//        }
//
//        [Test]
//        public async Task ConfigurationParsing_Works()
//        {
//            var provider = new WebApiConfigurationProvider(new WebApiProviderOptions
//            {
//                ApiKey = Guid.NewGuid().ToString(),
//                BaseUrl = "https://example.com",
//                RequestUrl = "/api/test"
//            });
//
//            await provider.LoadAsync(_httpClient);
//        }
//    }
//}