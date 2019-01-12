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
    /// File Decryption sample using a string private keyring.
    /// </summary>
    public class FileDecryptionSample
    {
        private string privateKeyRing = null;
        private char[] passphrase = null;

        public FileDecryptionSample(string privateKeyRing, char [] passphrase)
        {
            this.privateKeyRing = privateKeyRing;
            this.passphrase = passphrase;
        }

        public void Decrypt(string inFile, string outFile)
        {
            Console.WriteLine("Decrypting " + inFile + " to " + outFile + " using string keyring.");

            Stream inputStream = File.OpenRead(inFile);
            Stream outputStream = File.OpenWrite(outFile);

            byte[] bytes = Encoding.UTF8.GetBytes(privateKeyRing);
            MemoryStream memoryStreamKeyIn = new MemoryStream(bytes);

            Decryptor decryptor = new Decryptor(memoryStreamKeyIn, passphrase);
            decryptor.Decrypt(inputStream, outputStream);
            Console.WriteLine("Decryption done.");
        }

    }
}
