/* 
 * This file is part of Safester C# OpenPGP SDK.                                
 * Copyright(C) 2019,  KawanSoft SAS
 * (http://www.kawansoft.com). All rights reserved.                                
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

namespace Safester.CryptoLibrary.Src.Api
{
    /// <summary>
    /// Class to protect / unprotected a passphrase when storage on hard drive.
    /// </summary>
    public class PassphraseProctector
    {
        static byte[] s_aditionalEntropy = { 9, 8, 7, 6, 5, 0, 0, 1, 2, 3, 7, 8, 9, 10, 1, 8, 9, 10, 12, 99, 33, 21, 32 };

        /// <summary>
        /// Return a proctected passphrase in hexadecimal.
        /// </summary>
        /// <param name="myPassphrase">the passphrase to protect</param>
        /// <returns>the protected passphrase in hexadecimal.</returns>
        public static string Protect(char[] myPassphrase)
        {
            byte[] bArray = Encoding.UTF8.GetBytes(new String(myPassphrase));
            byte[] bArrayProtected = ProtectedData.Protect(bArray, s_aditionalEntropy, DataProtectionScope.CurrentUser);

            var hexString = HexConverter.ToHexString(bArrayProtected);
            return hexString;
        }

        /// <summary>
        /// Unprotects an hexidecimal protected string protected with Protect. 
        /// </summary>
        /// <param name="myPassphraseProtected"></param>
        /// <returns></returns>
        public static char[] Unprotect(string myPassphraseProtected)
        {
            byte[] bArrayProtected = HexConverter.ToByteArray(myPassphraseProtected);
            byte[] bArray = ProtectedData.Unprotect(bArrayProtected, s_aditionalEntropy, DataProtectionScope.CurrentUser);

            string utfString = Encoding.UTF8.GetString(bArray, 0, bArray.Length);
            return utfString.ToCharArray();
        }
    }
}
