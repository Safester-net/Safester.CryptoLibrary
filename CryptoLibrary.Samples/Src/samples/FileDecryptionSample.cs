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
    /// File Decryption sample using a string private keyring.
    /// </summary>
    public class FileDecryptionSample
    {
        private string privateKeyRing = null;
        private char[] passphrase = null;

        public FileDecryptionSample(string privateKeyRing, char [] passphrase)
        {
            this.privateKeyRing = privateKeyRing;
            this.passphrase = passphrase;
        }

        public void Decrypt(string inFile, string outFile)
        {
            Console.WriteLine("Decrypting " + inFile + " to " + outFile + " using string keyring.");

            Stream inputStream = File.OpenRead(inFile);
            Stream outputStream = File.OpenWrite(outFile);

            byte[] bytes = Encoding.UTF8.GetBytes(privateKeyRing);
            MemoryStream memoryStreamKeyIn = new MemoryStream(bytes);

            PgpDecryptor pgpDecryptor = new PgpDecryptor();
            pgpDecryptor.Decrypt(memoryStreamKeyIn, passphrase, inputStream, outputStream);
            Console.WriteLine("Decryption done.");
        }

    }
}
