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
        public void FromRequest_NullRequest_ReturnsNull()
        {
            var factory = GetQueryFactory();

            var result = factory.FromRequest(null);

            Assert.IsNull(result);
        }

        [TestCase(null, null)]
        [TestCase(null, 1)]
        [TestCase("test", null)]
        [TestCase("test", 1)]
        public void FromRequest_ValidRequest_ReturnDictionary(string searchString, int pageNumber)
        {
            var factory = GetQueryFactory();
            var request = new SearchRequest(searchString, pageNumber);

            var result = factory.FromRequest(request);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Values.Contains(searchString));
            Assert.IsTrue(result.Values.Contains(pageNumber));
        }
    }
}
