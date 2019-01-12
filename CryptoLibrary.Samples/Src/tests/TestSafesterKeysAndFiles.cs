using CryptoLibrary.Samples;
using CryptoLibrary.Samples.Src;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safester.CryptoLibrary.Samples.Src.tests
{
    public class TestSafesterKeysAndFiles
    {
        public static void Test()
        {
            // For safester tess
            string encryptedFileSafester = MainControler.basePath + "\\1182-d181213d-1544682969189_comptes.xlsx.pgp";
            string defaultPrivateKeyring = MainControler.basePath + "\\priv_key.txt";
            //string defaultPublicKeyring = MainControler.basePath + "\\pub_key.txt";

            Console.WriteLine(DateTime.Now + " File Decryption...");
            string privateKeyringSafester = File.ReadAllText(defaultPrivateKeyring);

            FileDecryptionSample fileDecryptionSampleSafester = new FileDecryptionSample(privateKeyringSafester, MainControler.passphrase.ToCharArray());
            string decryptedFileSafester = encryptedFileSafester.Substring(0, encryptedFileSafester.LastIndexOf("."));
            fileDecryptionSampleSafester.Decrypt(encryptedFileSafester, decryptedFileSafester);

            Console.WriteLine();
            Console.WriteLine("Press enter to close....");
            Console.ReadLine();
        }
    }
}
