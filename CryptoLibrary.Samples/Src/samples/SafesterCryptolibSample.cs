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
        public static void doIt()
        {
            PgpKeyPairGenerator pgpKeyPairGenerator = new PgpKeyPairGenerator("john@smith.com", "my_passphrase".ToCharArray(), PgpAsymAlgo.DSA_ELGAMAL, PgpAsymKeyLength.BITS_1024);
            PgpPairKeyring pgpPairKeyring = pgpKeyPairGenerator.Generate();

            String privateKeyring = pgpPairKeyring.PrivateKeyRing;
            String publicKeyring = pgpPairKeyring.PublicKeyRing;

            Console.WriteLine(privateKeyring);
            Console.WriteLine(publicKeyring);

            List<PgpPublicKey> encKeys = new List<PgpPublicKey>();
            encKeys.Add(PgpPublicKeyGetter.ReadPublicKey(publicKeyring));

            string basePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\safester_samples";
            string inFile = basePath + "\\koala.jpg";
            string outFile = basePath + "\\koala.jpg.pgp";

            Stream inputStream = File.OpenRead(inFile);
            Stream outputStream = File.OpenWrite(outFile);

            PgpEncryptor pgpEncryptor = new PgpEncryptor(false, true);
            pgpEncryptor.Encrypt(encKeys, inputStream, outputStream);
            Console.WriteLine("Encryption done.");

            string inFileEncrypted = outFile;
            string outFileDecrypted = basePath + "\\koala_2.jpg";

            inputStream = File.OpenRead(inFileEncrypted);
            outputStream = File.OpenWrite(outFileDecrypted);

            byte[] bytes = Encoding.UTF8.GetBytes(privateKeyring);
            MemoryStream memoryStreamKeyIn = new MemoryStream(bytes);

            PgpDecryptor PgpDecryptor = new PgpDecryptor(memoryStreamKeyIn, "my_passphrase".ToCharArray());
            PgpDecryptor.Decrypt(inputStream, outputStream);
            Console.WriteLine("Decryption done.");

            Console.WriteLine();
            Console.WriteLine("Press enter to close....");
            Console.ReadLine();
        }
    }
}
