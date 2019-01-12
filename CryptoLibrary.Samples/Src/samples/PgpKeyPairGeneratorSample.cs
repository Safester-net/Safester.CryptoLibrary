using Safester.CryptoLibrary.Api;
using System;

namespace CryptoLibrary.Samples.Src
{
    public class PgpKeyPairGeneratorSample
    {

        private PgpPairKeyring pgpPairKeyring = null;

        public PgpPairKeyring PgpPairKeyring { get => pgpPairKeyring; }

        public void Generate(string identity, string passphrase, PgpAsymAlgo pgpAsymAlgo, PgpAsymKeyLength pgpAsymKeyLength)
        {
            Console.WriteLine(DateTime.Now + " Starting generation...");
            PgpKeyPairGenerator pgpKeyPairGenerator = new PgpKeyPairGenerator(identity, passphrase.ToCharArray(), pgpAsymAlgo, pgpAsymKeyLength);
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