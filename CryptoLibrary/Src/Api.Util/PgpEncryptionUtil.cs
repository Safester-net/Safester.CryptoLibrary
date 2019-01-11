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
using Org.BouncyCastle.Bcpg.OpenPgp;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safester.CryptoLibrary.Api.Util
{
    class PgpEncryptionUtil
    {
        /// <summary>Write out the passed in file as a literal data packet in partial packet format.</summary>
        public static void WriteFileToLiteralData(
            Stream inputStream,
            Stream outputStream,
            char fileType,
            string fileName,
            byte[] buffer)
        {

            if (fileName == null)
            {
                fileName = "fileName";
            }

            try
            {

                PgpLiteralDataGenerator lData = new PgpLiteralDataGenerator();
                Stream pOut = lData.Open(outputStream, fileType, fileName, DateTime.Now, buffer);
                byte[] buf = new byte[buffer.Length];

                int len;
                while ((len = inputStream.Read(buf, 0, buf.Length)) > 0)
                {
                    pOut.Write(buf, 0, len);
                }

                lData.Close();
            }
            finally
            {
                inputStream.Dispose();
            }

        }

        public static SecureRandom getSecureRandom()
        {
            SecureRandom secureRandom = SecureRandom.GetInstance("SHA1PRNG", false);
            byte[] seed = new byte[1024];
            secureRandom.NextBytes(seed);
            secureRandom.SetSeed(seed);
            return secureRandom;
        }
    }
}
