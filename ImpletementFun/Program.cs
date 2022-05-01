using System;
using Function.Core.model;

namespace ImpletementFun
{
    class Program
    {
        static void Main(string[] args)
        {
            ProductModel pm = new ProductModel();
            try
            {    
                var result = pm.CheckProductExpireTime("ABCDE2022021522", new DateTime(2022,  02, 15, 21, 56, 00));
                Console.WriteLine(result);
                var result2 = pm.CheckProductExpireTime("EEEEE2022021523", new DateTime(2022, 02, 15, 22, 25, 00));
                Console.WriteLine(result2);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }
    }
}
