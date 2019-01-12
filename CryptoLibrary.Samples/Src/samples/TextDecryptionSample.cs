using Org.BouncyCastle.Bcpg.OpenPgp;
using Safester.CryptoLibrary.Api;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoLibrary.Samples
{
    /// <summary>
    /// Text encryption sample using a string public keyring.
    /// </summary>
    public class TextDecryptionSample
    {
        private string privateKeyRing = null;
        private char[] passphrase = null;

        public TextDecryptionSample(string privateKeyRing, char[] passphrase)
        {
            this.privateKeyRing = privateKeyRing;
            this.passphrase = passphrase;
        }

        public string Decrypt(string inText)
        {
            //Console.WriteLine("Decrypting " + inText + " using string keyring.");
            byte[] bytes = Encoding.UTF8.GetBytes(privateKeyRing);
            MemoryStream memoryStreamKeyIn = new MemoryStream(bytes);

            PgpDecryptor pgpDecryptor = new PgpDecryptor(memoryStreamKeyIn, passphrase);
            string outText = pgpDecryptor.Decrypt(inText);
            Console.WriteLine("Decryption done.");
            return outText;
        }

    }
}
