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
    public class TextEncryptionSample
    {
        private string publicKeyRing = null;
        private const bool armor = true;
        private bool withIntegrityCheck = true;

        public TextEncryptionSample(string publicKeyRing)
        {
            this.publicKeyRing = publicKeyRing;
        }

        public string Encrypt(string inText)
        {
            //Console.WriteLine("Encrypting " + inText + " using string keyring.");
            List<PgpPublicKey> encKeys = new List<PgpPublicKey>();
            encKeys.Add(PgpPublicKeyGetter.ReadPublicKey(this.publicKeyRing));

            PgpEncryptor pgpEncryptor = new PgpEncryptor(armor, withIntegrityCheck);
            string outText = pgpEncryptor.Encrypt(encKeys, inText);
            Console.WriteLine("Encryption done.");
            return outText;
        }

    }
}
