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

namespace Safester.CryptoLibrary.Samples.Src.samples
{
    public class SafesterCryptolibSample
    {
        public static void DoIt()
        {
            PgpKeyPairGenerator pgpKeyPairGenerator = new PgpKeyPairGenerator("john@smith.com", "my_passphrase".ToCharArray(), PublicKeyAlgorithm.DSA_ELGAMAL, PublicKeyLength.BITS_1024);
            PgpKeyPairHolder pgpKeyPairHolder = pgpKeyPairGenerator.Generate();

            String privateKeyring = pgpKeyPairHolder.PrivateKeyRing;
            String publicKeyring = pgpKeyPairHolder.PublicKeyRing;

            Console.WriteLine(privateKeyring);
            Console.WriteLine(publicKeyring);

            List<PgpPublicKey> encKeys = new List<PgpPublicKey>();
            encKeys.Add(PgpPublicKeyGetter.ReadPublicKey(publicKeyring));

            string basePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\safester_samples";
            string inFile = basePath + "\\koala.jpg";
            string outFile = basePath + "\\koala.jpg.pgp";

            Stream inputStream = File.OpenRead(inFile);
            Stream outputStream = File.OpenWrite(outFile);

            Encryptor encryptor = new Encryptor(false, true);
            encryptor.Encrypt(encKeys, inputStream, outputStream);
            Console.WriteLine("Encryption done.");

            string inFileEncrypted = outFile;
            string outFileDecrypted = basePath + "\\koala_2.jpg";

            inputStream = File.OpenRead(inFileEncrypted);
            outputStream = File.OpenWrite(outFileDecrypted);

            byte[] bytes = Encoding.UTF8.GetBytes(privateKeyring);
            MemoryStream memoryStreamKeyIn = new MemoryStream(bytes);

            Decryptor decryptor = new Decryptor(memoryStreamKeyIn, "my_passphrase".ToCharArray());
            decryptor.Decrypt(inputStream, outputStream);
            Console.WriteLine("Decryption done.");

            Console.WriteLine();
            Console.WriteLine("Press enter to close....");
            Console.ReadLine();
        }
    }
}
