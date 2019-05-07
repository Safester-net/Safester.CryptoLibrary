/* 
 * This file is part of Safester C# OpenPGP SDK.                                
 * Copyright(C) 2019,  KawanSoft SAS
 * (https://www.safester.net). All rights reserved.                                
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License. 
 */

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
