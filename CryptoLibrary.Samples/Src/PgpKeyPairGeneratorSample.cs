using Safester.CryptoLibrary.Api;
using System;

namespace CryptoLibrary.Samples.Src
{
    public class PgpKeyPairGeneratorSample
    {
        public const string IDENTIFY = "myemail@domain.com";
        public const string PASSPHRASE = "myPassphrase";
        public const PgpAsymAlgo PGP_ASYM_ALGO = PgpAsymAlgo.DSA_ELGAMAL;
        public const PgpAsymKeyLength PGP_ASYM_KEY_LENGTH = PgpAsymKeyLength.BITS_1024;

        private PgpPairKeyring pgpPairKeyring = null;

        public PgpPairKeyring PgpPairKeyring { get => pgpPairKeyring; }

        public void Generate()
        {
            Console.WriteLine(DateTime.Now + " Starting generation...");
            PgpKeyPairGenerator pgpKeyPairGenerator = new PgpKeyPairGenerator(IDENTIFY, PASSPHRASE.ToCharArray(),
                PGP_ASYM_ALGO, PGP_ASYM_KEY_LENGTH);
            pgpPairKeyring = pgpKeyPairGenerator.Generate();

            Console.WriteLine(DateTime.Now + " Done");
            Console.WriteLine();
            Console.WriteLine(PgpPairKeyring.PrivateKeyRing);
            Console.WriteLine();
            Console.WriteLine(PgpPairKeyring.PublicKeyRing);
            Console.WriteLine();
            Console.WriteLine("Press enter to close....");
            Console.ReadLine();
      
        }



    }
}