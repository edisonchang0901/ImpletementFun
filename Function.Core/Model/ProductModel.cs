﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Function.Core.model
{
    public class ProductModel
    {
       
        static Random random = new Random();
        public string GetProductNumber()
        {
            var productNumber = string.Empty;
            var item_code = string.Empty;
            for (int i = 1; i <= 5; i++)
            {
                item_code = item_code + GetLetter().ToString();
            }
            DateTime systemDateTime = DateTime.Now;
            productNumber = $"{item_code}{systemDateTime.ToString("yyyyMMddHH")}";
            return productNumber;
        }
        public string CheckProductExpireTime(string productNumber, DateTime systemDateTime)
        {
            var match = CreatMatch(productNumber);
            CheckProductName(match);
            DateTime productDateTime;
            CheckMatchProductDateTime(match, out productDateTime);        
            return GetProductExpireMessage(productDateTime, systemDateTime);
        }

        public string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            if (fi == null) return string.Empty;

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null && attributes.Any())
                return attributes[0].Description;
            else
                return value.ToString();
        }

        private Match CreatMatch(string productNumber)
        {
            string productNamePattern = @"(?=.{15}$)([A-Z]{5})([\d]{10})";
            Regex regexProductNameChecker = new Regex(productNamePattern, RegexOptions.Compiled);
            return regexProductNameChecker.Match(productNumber);
        }

        private char GetLetter()
        {
            Random seed = new Random();
            int number = (int)(0 + seed.NextDouble() * (90 - 65 + 1) + 65);
            return Convert.ToChar(number);
        }

        private void CheckProductName(Match match)
        {
            if (match.Success.Equals(false))
            {
                throw new ArgumentException(GetEnumDescription(ProductExpireStatus.None));
            }
        }

        private void CheckMatchProductDateTime(Match match, out DateTime productDateTime)
        {
            if (DateTime.TryParseExact(match.Groups[2].Value, "yyyyMMddHH", null, System.Globalization.DateTimeStyles.None, out productDateTime).Equals(false))
            {
                throw new ArgumentException(GetEnumDescription(ProductExpireStatus.None));
            }
        }

        private bool IsProductExpired(DateTime productDateTime, DateTime systemDateTime)
        {
            if (systemDateTime.AddMinutes(5) > productDateTime)
            {
                return true;
            }
            return false;
        }

        private bool IsProductExpiring(DateTime productDateTime, DateTime systemDateTime)
        {
            if (systemDateTime.AddMinutes(5) <= productDateTime && systemDateTime.AddMinutes(60) > productDateTime)
            {
                return true;
            }
            return false;
        }

        private string GetProductExpireMessage(DateTime productDateTime, DateTime systemDateTime)
        {
            if (IsProductExpired(productDateTime, systemDateTime))
            {
                return GetEnumDescription(ProductExpireStatus.ProductExpired);
            }
            if (IsProductExpiring(productDateTime, systemDateTime))
            {
                return GetEnumDescription(ProductExpireStatus.ProductExpiring);
            }
            return string.Empty;
        }


        public enum ProductExpireStatus
        {
            /// <summary>
            /// 商品條碼格式不符
            /// </summary>
            [Description("Product barcode format error")]
            None = 0,
            /// <summary>
            /// 商品已過期
            /// </summary>
            //[Description("商品已過期")]
            [Description("expired")]
            ProductExpired = 1,
            /// <summary>
            /// 商品即將到期
            /// </summary>
            //[Description("商品即將到期")]
            [Description("expiring")]
            ProductExpiring = 2,
        }
    }
}
