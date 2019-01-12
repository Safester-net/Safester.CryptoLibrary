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
using Safester.CryptoLibrary.Api;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoLibrary.Samples
{
    /// <summary>
    /// Text encryption sample using a string public keyring.
    /// </summary>
    public class TextDecryptionSample
    {
        private string privateKeyRing = null;
        private char[] passphrase = null;

        public TextDecryptionSample(string privateKeyRing, char[] passphrase)
        {
            this.privateKeyRing = privateKeyRing;
            this.passphrase = passphrase;
        }

        public string Decrypt(string inText)
        {
            //Console.WriteLine("Decrypting " + inText + " using string keyring.");
            byte[] bytes = Encoding.UTF8.GetBytes(privateKeyRing);
            MemoryStream memoryStreamKeyIn = new MemoryStream(bytes);

            Decryptor decryptor = new Decryptor(memoryStreamKeyIn, passphrase);
            string outText = decryptor.Decrypt(inText);
            Console.WriteLine("Decryption done.");
            return outText;
        }

    }
}
