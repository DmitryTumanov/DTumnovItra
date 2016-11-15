using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OnlinerTask.BLL.Services.Search.Request;
using OnlinerTask.BLL.Services.Search.Request.Implementations;
using Assert = NUnit.Framework.Assert;

namespace OnlinerTask.Tests
{
    [TestClass]
    public class RequestFactoryTests
    {
        public IRequestFactory GetRequestFactory()
        {
            return new OnlinerRequestFactory();
        }

        static readonly object[] TestDictionaries =
        {
            null,
            new Dictionary<string, object>
            {
                {"test", "test"}
            }
        };

        static readonly object[] TestEndpointsAndDictionaries =
        {
            new object[]
            {
                "http://test.com/test",
                null
            },
            new object[] {
                "https://test.com/test/test",
                new Dictionary<string, object>
                {
                    {"test", "test"}
                }
            }
        };

        static readonly object[] TestEndpointsAndDictionariesWithExpected =
        {
            new object[] {
                "http://test.com/test",
                "http://test.com/test?test1=0",
                new Dictionary<string, object>
                {
                    {"test1", 0}
                }
            },
            new object[] {
                "https://test.com/test/test",
                "https://test.com/test/test?test1=0&test2=0",
                new Dictionary<string, object>
                {
                    {"test1", 0},
                    {"test2", 0}
                }
            },
            new object[] {
                "https://test.com/test/test/test",
                "https://test.com/test/test/test?test1=0&test2=0&test3=0",
                new Dictionary<string, object>
                {
                    {"test1", 0},
                    {"test2", 0},
                    {"test3", 0}
                }
            },
        };

        [TestCaseSource(nameof(TestDictionaries))]
        public void CreateRequest_NullEndpoint_ReturnNull(IDictionary<string, object> parametersQuery)
        {
            var factory = GetRequestFactory();

            var result = factory.CreateRequest(null, parametersQuery);

            Assert.IsNull(result);
        }

        [TestCaseSource(nameof(TestEndpointsAndDictionaries))]
        public void CreateRequest_ValidEndpoint_ReturnNotNull(string endpoint, IDictionary<string, object> parametersQuery)
        {
            var factory = GetRequestFactory();

            var result = factory.CreateRequest(endpoint, parametersQuery);

            Assert.IsNotNull(result);
        }

        [TestCase("http://test.com/test", "http://test.com/test?")]
        [TestCase("https://test.com/test/test", "https://test.com/test/test?")]
        public void CreateRequest_NullDictionary_ReturnValidRequest(string endpoint, string expectedString)
        {
            var factory = GetRequestFactory();

            var result = factory.CreateRequest(endpoint, null);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Address.AbsoluteUri);
            Assert.AreEqual(expectedString, result.Address.AbsoluteUri);
        }

        [TestCaseSource(nameof(TestEndpointsAndDictionariesWithExpected))]
        public void CreateRequest_NotNullDictionary_ReturnValidRequest(string endpoint, string expectedString, IDictionary<string, object> parametersQuery)
        {
            var factory = GetRequestFactory();

            var request = factory.CreateRequest(endpoint, parametersQuery);

            Assert.IsNotNull(request);
            Assert.IsNotNull(request.Address.AbsoluteUri);
            Assert.AreEqual(expectedString, request.Address.AbsoluteUri);
        }
    }
}
