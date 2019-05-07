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

using Org.BouncyCastle.Bcpg.OpenPgp;
using Safester.CryptoLibrary.Api;
using System;
using System.Collections.Generic;
using System.IO;

namespace CryptoLibrary.Samples
{
    /// <summary>
    /// File encryption sample using a string public keyring.
    /// </summary>
    public class FileEncryptionSample
    {
        private string publicKeyRing = null;
        private bool armor = false;
        private bool withIntegrityCheck = true;

        public FileEncryptionSample(string publicKeyRing)
        {
            this.publicKeyRing = publicKeyRing;
        }

        public void Encrypt(string inFile, string outFile)
        {
            Console.WriteLine("Encrypting " + inFile + " to " + outFile + " using string keyring.");
            List<PgpPublicKey> encKeys = new List<PgpPublicKey>();
            encKeys.Add(PgpPublicKeyGetter.ReadPublicKey(this.publicKeyRing));

            Stream inputStream = File.OpenRead(inFile);
            Stream outputStream = File.OpenWrite(outFile);

            Encryptor encryptor = new Encryptor(armor, withIntegrityCheck);
            encryptor.Encrypt(encKeys, inputStream, outputStream);
            Console.WriteLine("Encryption done.");
        }

    }
}
