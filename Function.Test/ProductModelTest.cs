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
        [TestCase("ABCDE2022021522", "20220215215600", ProductExpireStatus.ProductExpired)]
        [TestCase("ABCDE2022021523", "20220215222500", ProductExpireStatus.ProductExpiring)]
        public void IsValidProductExpireTime(string input, string systemDateTime, ProductExpireStatus expected)
        {
            string result = _productModel.CheckProductExpireTime(input, DateTime.ParseExact(systemDateTime, "yyyyMMddHHmmss", null));
            Assert.AreEqual(result, _productModel.GetEnumDescription(expected));
        }

        [Test]
        [TestCase("", "20220501000000", ProductExpireStatus.None)]
        [TestCase("ABCDE123456789", "20220501000000", ProductExpireStatus.None)]
        [TestCase("ABCDE123456789", "20220501000000", ProductExpireStatus.None)]
        [TestCase("EEEEE1234567890", "20220501000000", ProductExpireStatus.None)]
        public void IsValidProductExpireTime_Throws(string input, string systemDateTime, ProductExpireStatus expected)
        {
            Assert.Throws<ArgumentException>(() => _productModel.CheckProductExpireTime(input, DateTime.ParseExact(systemDateTime, "yyyyMMddHHmmss", null)), _productModel.GetEnumDescription(expected));
        }


        [Test]
        [TestCase("IFaaaaaa20130101.1", "20151027000000", true)]
        [TestCase("IFmmmmmm20151027.2", "20151027000000", false)]
        public void IsValidDeleteFileBySystemTime(string input, string systemDateTime, bool expected) 
        {
            bool result = _productModel.DeleteFileBySystemTime(input, -6, DateTime.ParseExact(systemDateTime, "yyyyMMddHHmmss", null));
            Assert.AreEqual(result, expected);
        }


        [Test]
        [TestCase("IFmmmmm20151027.2", "20151027000000", "file format error")]
        [TestCase("IFmmmmm12345678.2", "20151027000000", "商品條碼格式不符")]
        [TestCase("IFaaaaaa20130101.9", "20151027000000", "file format error")]
        
        public void IsValidDeleteFileBySystemTime_Throws(string input, string systemDateTime, string expected)
        {
            Assert.Throws<ArgumentException>(() => _productModel.DeleteFileBySystemTime(input, -6, DateTime.ParseExact(systemDateTime, "yyyyMMddHHmmss", null)), expected);
        }

    }
}
