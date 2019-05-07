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
using System.Text;

namespace Safester.CryptoLibrary.Src.Api.Util
{
    /// <summary>
    /// A sample tha shows how to use HexConverter.
    /// </summary>
    public class HexConverterSample
    {
        /// <summary>
        /// Displays on console a convert to anf from hex.
        /// </summary>
        public static void Convert()
        {
            char[] myPassphrase = "myPassphrase".ToCharArray();
            byte[] bArray = Encoding.UTF8.GetBytes(new String(myPassphrase));
            string hex = HexConverter.ToHexString(bArray);
            Console.WriteLine("hex      : " + hex);

            byte[] byteArrayback = HexConverter.ToByteArray(hex);
            string utfString = Encoding.UTF8.GetString(byteArrayback, 0, byteArrayback.Length);
            Console.WriteLine("utfString: " + utfString + ":");
            Console.WriteLine();

            Console.WriteLine("Press enter to close....");
            Console.ReadLine();
        }

    }
}
