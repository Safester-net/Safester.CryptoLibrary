
using Safester.CryptoLibrary.Api;
using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safester.CryptoLibrary.Samples
{
    class TextEncryptionSample
    {
    
        public static void EncryptAndDecryptText()
        {
            string passphrase = "*2loveme$123";
            string pubKeyPath = "c:\\test\\bouncy\\pub_key.txt";
            string privKeyPath = "c:\\test\\bouncy\\priv_key.txt";

            String inputText = "Longtemps je me suis couché de bonne heure.";

            // Add a PgpPubliKey using a keyring file
            Stream inputStream = File.OpenRead(pubKeyPath);
            List<PgpPublicKey> encKeys = new List<PgpPublicKey>();
            encKeys.Add(PgpPublicKeyGetter.ReadPublicKey(inputStream));

            // Encrypt
            PgpEncryptor pgpEncryptor = new PgpEncryptor();
            String encryptedArmorText = pgpEncryptor.Encrypt(encKeys, inputText);

            Console.WriteLine(inputText);
            Console.WriteLine();
            Console.WriteLine(encryptedArmorText);
            Console.WriteLine();

            // Decrypt
            Stream privateKeyRing = File.OpenRead(privKeyPath);
            PgpDecryptor pgpDecryptor = new PgpDecryptor();
            String decryptedText = pgpDecryptor.Decrypt(privateKeyRing, passphrase.ToCharArray(), encryptedArmorText);
            Console.WriteLine(decryptedText);

            Console.WriteLine();
            Console.WriteLine("Press enter to close....");
            Console.ReadLine();
        }

       
       
    }
}
