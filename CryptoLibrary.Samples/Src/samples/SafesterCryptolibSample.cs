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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safester.CryptoLibrary.Samples.Src.samples
{
    public class SafesterCryptolibSample
    {
        public static void DoIt()
        {
            string identity = "john@smith.com";
            char[] passphrase = "my_passphrase".ToCharArray();

            PgpKeyPairGenerator generator = 
                new PgpKeyPairGenerator(identity, passphrase, PublicKeyAlgorithm.RSA, PublicKeyLength.BITS_2048);
            PgpKeyPairHolder pgpKeyPairHolder = generator.Generate();

            String privateKeyring = pgpKeyPairHolder.PrivateKeyRing;
            String publicKeyring = pgpKeyPairHolder.PublicKeyRing;

            Console.WriteLine(privateKeyring);
            Console.WriteLine(publicKeyring);

            PgpPublicKey pgpPublicKey = PgpPublicKeyGetter.ReadPublicKey(publicKeyring);
            List<PgpPublicKey> encKeys = new List<PgpPublicKey>();
            encKeys.Add(pgpPublicKey);

            string rootDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string inFile = rootDir + "\\safester_samples\\koala.jpg";
            string outFile = rootDir + "\\safester_samples\\koala.jpg.pgp";

            // This sample runs on Windows. 
            // Use System.IO.File to open the in and out streams
            Stream inputStream = File.OpenRead(inFile);
            Stream outputStream = File.OpenWrite(outFile);

            bool armor = false;
            bool withIntegrityCheck = true;

            // Create an Encryptor instance and pass the public keys and streams
            Encryptor encryptor = new Encryptor(armor, withIntegrityCheck);
            encryptor.Encrypt(encKeys, inputStream, outputStream);
            Console.WriteLine("Encryption done.");

            string inFileEncrypted = rootDir + "\\safester_samples\\koala.jpg.pgp"; ;
            string outFileDecrypted = rootDir + "\\safester_samples\\koala_2.jpg";

            inputStream = File.OpenRead(inFileEncrypted);
            outputStream = File.OpenWrite(outFileDecrypted);

            Decryptor decryptor = new Decryptor(privateKeyring, passphrase);
            decryptor.Decrypt(inputStream, outputStream);
            Console.WriteLine("Decryption integrity check status: " + decryptor.Verify);
            Console.WriteLine("Decryption done.");

            String inText = "For a long time I would go to bed early. Sometimes, " +
                "the candle barely out, my eyes close so quickly that I did not have " +
                "time to tell myself \"I’m falling asleep.\"";
            encryptor = new Encryptor(armor, withIntegrityCheck);
            string outText = encryptor.Encrypt(encKeys, inText);
            Console.WriteLine("Encryption done.");
            Console.WriteLine(outText);

            decryptor = new Decryptor(privateKeyring, passphrase);
            string decryptText = decryptor.Decrypt(outText);
            Console.WriteLine("Decryption integrity check status: " + decryptor.Verify);
            Console.WriteLine(decryptText);

            decryptText = decryptor.Decrypt(outText);
            Console.WriteLine(decryptText);

            decryptText = decryptor.Decrypt(outText);
            Console.WriteLine(decryptText);

            Console.WriteLine();
            Console.WriteLine("Press enter to close....");
            Console.ReadLine();
        }
    }
}
