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
using System.Text;

namespace Safester.CryptoLibrary.Samples.Src.samples
{
    public class TestKeySignatureVerifySample
    {
        public static void TestKeySignature()
        {
            string rootDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

            string inFile = rootDir + "\\safester_samples\\pub_key.txt";
            string publicKeyRing = File.ReadAllText(inFile);

            inFile = rootDir + "\\safester_samples\\master_pub_key.txt";
            string masterPublicKeyRing = File.ReadAllText(inFile);

            Console.WriteLine("Testing key signature...");
            PgpPublicKeyVerifier pgpPublicKeyVerifier = new PgpPublicKeyVerifier();
            bool verify = pgpPublicKeyVerifier.VerifySignature(publicKeyRing, masterPublicKeyRing);

            Console.WriteLine("Verify: " + verify);
            if (pgpPublicKeyVerifier.GetException() != null)
            {
                Console.WriteLine("Verify Exception " + pgpPublicKeyVerifier.GetException());
            }

            Console.WriteLine();
            Console.WriteLine("Press enter to close....");
            Console.ReadLine();

        }

        public static void SafesterSendMessageCodeSample()
        {
            List<PgpPublicKey> encKeys = new List<PgpPublicKey>();
            encKeys = MyCallGetPublicKeys();

            // Get the PGP key from server with /getPublicKey 
            // and userEmailAddr = "contact@safelogic.com"
            String masterPgpPublicKeyring = MyCallGetPublicKey("contact@safelogic.com");

            // Send message for all users
            List<String> recipientEmails = MyGetRecipients();
            List<String> recipientEmailsValidated = new List<String>();

            for (int i = 0; i < recipientEmails.Count; i++)
            {
                String theRecipientEmail = recipientEmails[i];
                PgpPublicKey theRecipientPgpPublicKey = encKeys[i];

                PgpPublicKeyVerifier verifier = new PgpPublicKeyVerifier();
                bool verify = verifier.VerifySignature(theRecipientPgpPublicKey, 
                    masterPgpPublicKeyring);

                if (!verify)
                {
                    Console.WriteLine("ALERT! Recipient PGP Public key is invalid." +
                        " Mail will not be sent for: " + theRecipientEmail);
                }
                else
                {
                    // Recipient PGP public is OK and authenticated
                    recipientEmailsValidated.Add(theRecipientEmail);
                }
            }

            // OK, send the email only to validated recipients
            MySendMessage(recipientEmailsValidated);
        }

        private static void MySendMessage(List<string> recipientEmailsValidated)
        {
            throw new NotImplementedException();
        }

        private static List<PgpPublicKey> MyCallGetPublicKeys()
        {
            throw new NotImplementedException();
        }

        private static string MyCallGetPublicKey(string v)
        {
            throw new NotImplementedException();
        }

        private static List<String> MyGetRecipients()
        {
            throw new NotImplementedException();
        }
    }
}
