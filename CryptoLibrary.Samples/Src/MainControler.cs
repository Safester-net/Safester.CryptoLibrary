
using Safester.CryptoLibrary.Api;
using Safester.CryptoLibrary.Samples.Src.tests;
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
        public static PgpAsymAlgo pgpAsymAlgo = PgpAsymAlgo.DSA_ELGAMAL;
        public static PgpAsymKeyLength pgpAsymKeyLength = PgpAsymKeyLength.BITS_1024;

        public static void Main(string[] args)
        {
            passphrase = File.ReadAllText(basePath + "\\password.txt");

            Console.WriteLine(DateTime.Now + " Generate key pair...");
            PgpKeyPairGeneratorSample pgpKeyPairGeneratorSample = new PgpKeyPairGeneratorSample();
            pgpKeyPairGeneratorSample.Generate(identity, passphrase, pgpAsymAlgo, pgpAsymKeyLength);
            PgpPairKeyring pgpPairKeyring = pgpKeyPairGeneratorSample.PgpPairKeyring;

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

            Console.WriteLine(DateTime.Now + " Text decryption...");
            TextDecryptionSample textDecryptionSample = new TextDecryptionSample(pgpPairKeyring.PrivateKeyRing, passphrase.ToCharArray());
            string clearText_2 = textDecryptionSample.Decrypt(encryptedText);

            Console.WriteLine(DateTime.Now + " Clear Text 1:");
            Console.WriteLine(DateTime.Now + " " + clearText_2);

            //Test a Safester decryption with an existing Keyring
            TestSafesterKeysAndFiles.Test();
        }

    }
}
