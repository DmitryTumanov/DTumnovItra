using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OnlinerTask.BLL.Services.Search.Request.RequestQueryFactory;
using OnlinerTask.BLL.Services.Search.Request.RequestQueryFactory.Implementations;
using OnlinerTask.Data.Requests;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace OnlinerTask.Tests
{
    [TestClass]
    public class RequestQueryFactoryTests
    {
        public IRequestQueryFactory GetQueryFactory()
        {
            return new OnlinerRequestQueryFactory();
        }

        [TestMethod]
        public void RequestQueryFactory_FromRequest_ReturnNull()
        {
            Assert.IsNull(GetQueryFactory().FromRequest(null));
        }

        [TestCase(null, null)]
        [TestCase(null, 1)]
        [TestCase("test", null)]
        [TestCase("test", 1)]
        public void RequestQueryFactory_FromRequest_ReturnDictionary(string searchString, int pageNumber)
        {
            var request = new SearchRequest(searchString, pageNumber);
            var dictionary = GetQueryFactory().FromRequest(request);
            Assert.IsNotNull(dictionary);
            Assert.IsTrue(dictionary.Values.Contains(searchString));
            Assert.IsTrue(dictionary.Values.Contains(pageNumber));
        }
    }
}
