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
using System.Text;

namespace Safester.CryptoLibrary.Src.Api
{
    /// <summary>
    /// Hexadecimal conversion tool.
    /// </summary>
    public class HexConverter
    {
        /// <summary>
        /// Converts a byte array to an hexadecimal string.
        /// </summary>
        /// <param name="bArray">the byte array</param>
        /// <returns>a hexadeciaml string</returns>
        public static string ToHexString(byte[] bArray)
        {
            var hexString = BitConverter.ToString(bArray);
            hexString = hexString.Replace("-", "");
            return hexString;
        }

        /// <summary>
        /// Converts an hexadecimal tring to byte array.
        /// </summary>
        /// <param name="hexString">the hex string</param>
        /// <returns>a byte array</returns>
        public static byte[] ToByteArray(String hexString)
        {
            byte[] retval = new byte[hexString.Length / 2];
            for (int i = 0; i < hexString.Length; i += 2)
                retval[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 16);
            return retval;
        }

    }
}
