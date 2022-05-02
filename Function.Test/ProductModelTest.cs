using System;
using Function.Core.model;
using NUnit.Framework;
using static Function.Core.model.ProductModel;

namespace Function.Test
{
    [TestFixture()]
    public class ProductModelTest
    {
        private ProductModel _productModel = null;

        [SetUp]
        public void Setup()
        {
            _productModel = new ProductModel();
        }

        [Test]
        //[TestCase("ABCDE123456789", ProductExpireStatus.None)]
        //[TestCase("ABCDE2022042716", ProductExpireStatus.ProductExpired)]
        [TestCase("ABCDE2022042717", ProductExpireStatus.ProductExpired)]
        public void IsValidProductExpireTime(string input, ProductExpireStatus expected)
        {
            string result = _productModel.CheckProductExpireTime(input, DateTime.Now);
            Assert.AreEqual(result, _productModel.GetEnumDescription(expected));
        }

        [Test]
        [TestCase("", ProductExpireStatus.None)]
        [TestCase("ABCDE123456789", ProductExpireStatus.None)]
        [TestCase("EEEEE1234567890", ProductExpireStatus.None)]
        public void IsValidProductExpireTime_Throws(string input, ProductExpireStatus expected)
        {
            Assert.Throws<ArgumentException>(() => _productModel.CheckProductExpireTime(input, DateTime.Now), _productModel.GetEnumDescription(expected));
        }


    }
}
