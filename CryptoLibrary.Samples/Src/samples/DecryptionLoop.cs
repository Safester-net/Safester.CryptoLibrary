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

namespace CryptoLibrary.Samples
{
    class DecryptionLoop
    {
        public const bool DO_ENCRYPT_SUBJECT = false;
        public const string identity = "myemail@domain.com";
        public static string passphrase = "myemail@domain.com";
        public static PublicKeyAlgorithm pgpAsymAlgo = PublicKeyAlgorithm.RSA;
        public static PublicKeyLength pgpAsymKeyLength = PublicKeyLength.BITS_2048;

        public static void DoIt()
        {
            Console.WriteLine(DateTime.Now + " Generate key pair...");
            PgpKeyPairGenerator pgpKeyPairGenerator = new PgpKeyPairGenerator(identity, passphrase.ToCharArray(), pgpAsymAlgo, pgpAsymKeyLength);
            PgpKeyPairHolder pgpKeyPairHolder = pgpKeyPairGenerator.Generate();

            List<PgpPublicKey> encKeys = new List<PgpPublicKey>();
            encKeys.Add(PgpPublicKeyGetter.ReadPublicKey(pgpKeyPairHolder.PublicKeyRing));

            Encryptor encryptor = new Encryptor(true, false);

            List<String> encryptedvalues = new List<string>();

            const int cpt = 200;
            Console.WriteLine(DateTime.Now + " Text encryption... In loop " + cpt + " times:");

            for (int i = 0; i < cpt; i++)
            {
                string clearValue = "This_is_a_full_plain_subject_for_an_email_text_" + i;
                string encryptedValue = encryptor.Encrypt(encKeys, clearValue);
                encryptedvalues.Add(encryptedValue);
            }
            Console.WriteLine(DateTime.Now + " Text encryption done!");
            Console.WriteLine();

            Decryptor decryptor = new Decryptor(pgpKeyPairHolder.PrivateKeyRing, passphrase.ToCharArray());
            Console.WriteLine(DateTime.Now + " Starting decryption... In loop " + cpt + " times:");

            DateTime dtBegin = DateTime.Now;
  
            List <String> decryptedValues = new List<string>();
            for (int i = 0; i < cpt; i++)
            {
                string decryptedValue = decryptor.Decrypt(encryptedvalues[i]);
                decryptedValues.Add(decryptedValue);
            }

            DateTime dtEnd = DateTime.Now;
            TimeSpan span = dtEnd - dtBegin;
            int ms = (int)span.TotalMilliseconds;

            Console.WriteLine(DateTime.Now + " Text decryption done!");
            Console.WriteLine(DateTime.Now + " Decryption Elasped in Milliseconds: " + ms);

            Console.WriteLine("Press enter to continue....");
            Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("Display decrypted values: ");

            for (int i = 0; i < cpt; i++)
            {
                Console.WriteLine(decryptedValues[i]);
            }

            Console.WriteLine();
            Console.WriteLine("Done!");
            Console.WriteLine("Press enter to close....");
            Console.ReadLine();

        }
    }
}
