using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Safester.CryptoLibrary.Src.Api.Util
{
    /// <summary>
    /// SHA Utilities.
    /// </summary>
    public class ShaUtil
    {
        public static string Compute(List<string> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list is null!");
            }
            if (list.Count == 0)
            {
                throw new ArgumentNullException("list contains no elements!");
            }

            String result = "";
            foreach(string str in list)
            {
                result += GetSha(str);
            }

            string finalResult = GetSha(result);
            return finalResult;
        }

        private static string GetSha(string str)
        {
            SHA1 sha1 = SHA1.Create();
            byte[] bArray = Encoding.UTF8.GetBytes(str);
            byte[] bHashArray = sha1.ComputeHash(bArray);
            string result = Encoding.UTF8.GetString(bHashArray);
            return result;
        }
    }
}
