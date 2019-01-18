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
    }
}
