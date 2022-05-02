using System;
using System.Collections.Generic;
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
                var result = pm.CheckProductExpireTime("ABCDE2022021522", new DateTime(2022, 02, 15, 21, 56, 00));
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
         
            List<string> fileList = new List<string>();
            #region createFileList
            fileList.Add("IFaaaaaa20130101.1");
            fileList.Add("IFbbbbbb20141201.2");
            fileList.Add("IFbbbbbb20150101.3");
            fileList.Add("IFdddddd20151019.1");
            fileList.Add("IFeeeeee20151019.1");
            fileList.Add("IFffffff20151020.2");
            fileList.Add("IFgggggg20151021.3");
            fileList.Add("IFhhhhhh20151022.4");
            fileList.Add("IFiiiiii20151023.5");
            fileList.Add("IFjjjjjj20151024.6");
            fileList.Add("IFkkkkkk20151025.7");
            fileList.Add("IFllllll20151026.1");
            fileList.Add("IFmmmmmm20151027.2");
            #endregion
            List<string> failFileList = new List<string>();
            foreach (string fileName in fileList)
            {
                try
                {
                    bool IsDelete = pm.DeleteFileBySystemTime(fileName, -6, new DateTime(2015, 10, 27, 00, 00, 00));
                    if (false.Equals(IsDelete)) 
                    {
                        Console.WriteLine(fileName);
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message.ToString());
                    failFileList.Add(fileName);
                }
                catch (Exception ex) 
                {
                    Console.WriteLine(ex.Message.ToString());
                }
            }
        }
    }
}
