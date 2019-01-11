
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
    class FileEncryptionSample
    {
        enum Operaton
        {
            ENCRYPTION,
            DECRYPTION,
        }

        public static void Main(string[] args)
        {

            TextEncryptionSample.EncryptAndDecryptText();

            string[] myArgs = new string[4];

            Operaton cipherOperation = Operaton.DECRYPTION;

            if (cipherOperation == Operaton.ENCRYPTION) 
            {
                myArgs[0] = "-e";
                myArgs[1] = "-i";
                myArgs[2] = "c:\\test\\bouncy\\koala.jpg";
                myArgs[3] = "c:\\test\\bouncy\\pub_key.txt";
                Console.WriteLine("Encrypt file " + myArgs[2]);
            }
            else
            {
                myArgs[0] = "-d";
                myArgs[1] = "c:\\test\\bouncy\\koala.jpg.pgp";
                myArgs[2] = "c:\\test\\bouncy\\priv_key.txt";
                myArgs[3] = "*2loveme$123";

                Console.WriteLine("Decrypt file " + myArgs[1]);
            }

            MainDoit(myArgs);

            Console.WriteLine();
            Console.WriteLine("Press enter to close....");
            Console.ReadLine();
        }

        public static void MainDoit(
            string[] args)
        {

            if (args.Length == 0)
            {
                Console.Error.WriteLine("usage: KeyBasedLargeFileProcessor -e|-d [-a|ai] file [secretKeyFile passPhrase|pubKeyFile]");
                return;
            }

            if (args[0].Equals("-e"))
            {
                Stream keyIn = null;
                Stream fos = null;
                if (args[1].Equals("-a") || args[1].Equals("-ai") || args[1].Equals("-ia"))
                {
                    keyIn = File.OpenRead(args[3]);
                    fos = File.Create(args[2] + ".pgp");

                    // Add a PgpPubliKey using a keyring file
                    Stream inputStream = File.OpenRead(args[2]);
                    List<PgpPublicKey> encKeys = new List<PgpPublicKey>();
                    encKeys.Add(PgpPublicKeyGetter.ReadPublicKey(keyIn));

                    Boolean armor = true;
                    Boolean withIntegrityCheck = (args[1].IndexOf('i') > 0);

                    PgpEncryptor pgpEncryptor = new PgpEncryptor(armor, withIntegrityCheck);
                    pgpEncryptor.Encrypt(encKeys, inputStream, fos);
                }
                else if (args[1].Equals("-i"))
                {
                    fos = File.Create(args[2] + ".pgp");

                    Stream inputStream = File.OpenRead(args[2]);
                    List<PgpPublicKey> encKeys = new List<PgpPublicKey>();

                    // Add a PgpPubliKey using a keyring content as test
                    String content = File.ReadAllText(args[3]);
                    encKeys.Add(PgpPublicKeyGetter.ReadPublicKey(content));

                    PgpEncryptor pgpEncryptor = new PgpEncryptor();
                    pgpEncryptor.Encrypt(encKeys, inputStream, fos);
                }
                else
                {
                    keyIn = File.OpenRead(args[2]);
                    fos = File.Create(args[1] + ".pgp");

                    Stream inputStream = File.OpenRead(args[0]);

                    List<PgpPublicKey> encKeys = new List<PgpPublicKey>();
                    encKeys.Add(PgpPublicKeyGetter.ReadPublicKey(keyIn));

                    PgpEncryptor pgpEncryptor = new PgpEncryptor(false, true);
                    pgpEncryptor.Encrypt(encKeys, inputStream, fos);
                }

                if (keyIn != null)
                {
                    keyIn.Close();
                }

                if (fos != null)
                {
                    fos.Close();
                }

            }
            else if (args[0].Equals("-d"))
            {
                Stream fis = File.OpenRead(args[1]);
                Stream keyIn = null;

                keyIn = File.OpenRead(args[2]);

                string filepath = args[1].Substring(0, args[1].LastIndexOf("."));
                Stream fos = File.OpenWrite(filepath);

                string keyring = File.ReadAllText(args[2]);
                byte[] bytes = Encoding.UTF8.GetBytes(keyring);
                MemoryStream memoryStreamKeyIn = new MemoryStream(bytes);

                PgpDecryptor pgpDecryptor = new PgpDecryptor();
                pgpDecryptor.Decrypt(memoryStreamKeyIn, args[3].ToCharArray(), fis, fos);

                if (keyIn != null)
                {
                    keyIn.Close();
                }

                if (fos != null)
                {
                    fos.Close();
                }
            }
            else
            {
                Console.Error.WriteLine("usage: KeyBasedLargeFileProcessor -d|-e [-a|ai] file [secretKeyFile passPhrase|pubKeyFile]");
            }
        }
    }
}
