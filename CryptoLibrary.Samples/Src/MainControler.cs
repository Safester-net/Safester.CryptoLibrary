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

using Safester.CryptoLibrary.Api;
using Safester.CryptoLibrary.Samples.Src.samples;
using Safester.CryptoLibrary.Samples.Src.tests;
using Safester.CryptoLibrary.Src.Api;
using Safester.CryptoLibrary.Src.Api.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoLibrary.Samples.Src
{
    public class MainControler
    {

        public static string clearText = "Longtemps je me suis couché de bonne heure.";
        public static string basePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\safester_samples";

        public static string clearFile = basePath + "\\koala.jpg";
        public static string clearFile_2 = basePath + "\\koala_2.jpg";
        public static string encryptedFile = basePath + "\\koala.jpg.pgp";

        public const string identity = "myemail@domain.com";
        public static string passphrase = null;
        public static PublicKeyAlgorithm pgpAsymAlgo = PublicKeyAlgorithm.DSA_ELGAMAL;
        public static PublicKeyLength pgpAsymKeyLength = PublicKeyLength.BITS_1024;

        public static void Main(string[] args)
        {
            TestAll();
        }


        public static void TestAll()
        {
            passphrase = File.ReadAllText(basePath + "\\password.txt");

            Console.WriteLine(DateTime.Now + " Generate key pair...");
            PgpKeyPairGeneratorSample pgpKeyPairGeneratorSample = new PgpKeyPairGeneratorSample();
            pgpKeyPairGeneratorSample.Generate(identity, passphrase, pgpAsymAlgo, pgpAsymKeyLength);
            PgpKeyPairHolder pgpPairKeyring = pgpKeyPairGeneratorSample.PgpKeyPairHolder;

            Console.WriteLine(DateTime.Now + " File Encryption...");
            FileEncryptionSample fileEncryptionSample = new FileEncryptionSample(pgpPairKeyring.PublicKeyRing);
            fileEncryptionSample.Encrypt(clearFile, encryptedFile);

            Console.WriteLine(DateTime.Now + " File Decryption...");
            FileDecryptionSample fileDecryptionSample = new FileDecryptionSample(pgpPairKeyring.PrivateKeyRing, passphrase.ToCharArray());
            fileDecryptionSample.Decrypt(encryptedFile, clearFile_2);

            Console.WriteLine(DateTime.Now + " Text encryption...");
            Console.WriteLine(DateTime.Now + " Clear Text:");
            Console.WriteLine(DateTime.Now + " " + clearText);

            TextEncryptionSample textEncryptionSample = new TextEncryptionSample(pgpPairKeyring.PublicKeyRing);
            string encryptedText = textEncryptionSample.Encrypt(clearText);
            Console.WriteLine(DateTime.Now + " Encrypted Text:");
            Console.WriteLine(DateTime.Now + " " + encryptedText);

            Decryptor decryptor = new Decryptor(pgpPairKeyring.PrivateKeyRing, passphrase.ToCharArray());
            string clearText_2 = decryptor.Decrypt(encryptedText);

            Console.WriteLine(DateTime.Now + " Clear Text decrypted after loop:");
            Console.WriteLine(DateTime.Now + " " + clearText_2);

            //Test a Safester decryption with an existing Keyring
            TestSafesterKeysAndFiles.Test();

            TestKeySignatureVerifySample.TestKeySignature();
        }
    }
}
