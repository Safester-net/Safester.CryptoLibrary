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
    /// File encryption sample using a string public keyring.
    /// </summary>
    public class FileEncryptionSample
    {
        private string publicKeyRing = null;
        private bool armor = false;
        private bool withIntegrityCheck = true;

        public FileEncryptionSample(string publicKeyRing)
        {
            this.publicKeyRing = publicKeyRing;
        }

        public void Encrypt(string inFile, string outFile)
        {
            Console.WriteLine("Encrypting " + inFile + " to " + outFile + " using string keyring.");
            List<PgpPublicKey> encKeys = new List<PgpPublicKey>();
            encKeys.Add(PgpPublicKeyGetter.ReadPublicKey(this.publicKeyRing));

            Stream inputStream = File.OpenRead(inFile);
            Stream outputStream = File.OpenWrite(outFile);

            PgpEncryptor pgpEncryptor = new PgpEncryptor(armor, withIntegrityCheck);
            pgpEncryptor.Encrypt(encKeys, inputStream, outputStream);
            Console.WriteLine("Encryption done.");
        }

    }
}
