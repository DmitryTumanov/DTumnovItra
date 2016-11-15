using System;
using System.Collections.Generic;
using System.Linq;
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

        public IDictionary<string, object> GetDictionary(IEnumerable<string> keys)
        {
            var random = new Random();
            IDictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (var elem in keys)
            {
                dictionary.Add(elem, random.Next());
            }
            return dictionary;
        }

        [TestCase(null, true)]
        [TestCase(null, false)]
        public void CreateRequest_NullEndpoint_ReturnNull(string endpoint, bool nullQueryFlag)
        {
            IDictionary<string, object> parametersQuery = nullQueryFlag ? null : new Dictionary<string, object>();
            Assert.IsNull(GetRequestFactory().CreateRequest(endpoint, parametersQuery));
        }

        [TestCase("http://test.com/test", true)]
        [TestCase("https://test.com/test/test", false)]
        public void CreateRequest_ValidEndpoint_ReturnNotNull(string endpoint, bool nullQueryFlag)
        {
            IDictionary<string, object> parametersQuery = nullQueryFlag ? null : new Dictionary<string, object>();
            Assert.IsNotNull(GetRequestFactory().CreateRequest(endpoint, parametersQuery));
        }

        [TestCase("http://test.com/")]
        [TestCase("http://test.com/test")]
        [TestCase("https://test.com/test/test")]
        public void CreateRequest_NullDictionary_ReturnValidRequest(string endpoint)
        {
            var request = GetRequestFactory().CreateRequest(endpoint, null);

            Assert.IsNotNull(request);
            Assert.IsNotNull(request.Address.AbsoluteUri);
            Assert.AreEqual($"{endpoint}?", request.Address.AbsoluteUri);
        }

        [TestCase("http://test.com/", "test1")]
        [TestCase("http://test.com/test", "test1", "test2")]
        [TestCase("https://test.com/test/test", "test1", "test2", "test3")]
        [TestCase("https://test.com/test/test/test", "test1", "test2", "test3", "test4")]
        public void CreateRequest_NotNullDictionary_ReturnValidRequest(string endpoint, params string[] parameters)
        {
            var dictionary = GetDictionary(parameters);
            var request = GetRequestFactory().CreateRequest(endpoint, dictionary);
            var expectedString = $"{endpoint}?{string.Join("&", dictionary.Select(k => $"{k.Key}={k.Value}"))}";

            Assert.IsNotNull(request);
            Assert.IsNotNull(request.Address.AbsoluteUri);
            Assert.AreEqual(expectedString, request.Address.AbsoluteUri);
        }
    }
}
