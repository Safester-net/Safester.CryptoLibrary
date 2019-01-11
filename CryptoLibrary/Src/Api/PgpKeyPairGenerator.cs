using Org.BouncyCastle.Bcpg;
using Org.BouncyCastle.Bcpg.OpenPgp;
using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safester.CryptoLibrary.Api
{
    /// <summary>
    /// Allows to generate a PGP Key Pair that will be use for Register. 
    /// Will create a strign armored keyring for both the
    /// private and public PGP key.
    /// </summary>
    public class PgpKeyPairGenerator
    {
        /** Default values */
        private const SymmetricKeyAlgorithmTag symmetricKeyAlgorithmTag = SymmetricKeyAlgorithmTag.Aes256;
        private int pgpSignature = PgpSignature.DefaultCertification;

        /** Updatable values */
        private PgpAsymAlgo pgpAsymAlgo = PgpAsymAlgo.DSA_ELGAMAL;
        private PgpAsymKeyLength pgpAsymKeyLength = PgpAsymKeyLength.LENGTH_2048;
        private DateTime expirationDate = DateTime.UtcNow;

        private string identity = null;
        private char[] passphrase = null;

        private AsymmetricKeyParameter publicKey = null;
        private AsymmetricKeyParameter privateKey = null;

        /// <summary>
        /// Constructor that will used defaults values for keyrings:
        /// - DSA/Elgamal
        /// - 2048 asym key size
        /// - AES/256 for sym algo an key size (not updatable)
        /// - Never expires (Not updatable)
        /// - 
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="passphrase"></param>
        public PgpKeyPairGenerator(string identity, char[] passphrase)
        {
            this.identity = identity;
            this.passphrase = passphrase;
        }

        /// <summary>
        /// Generates the armored private and public keyrings. 
        /// </summary>
        /// <returns></returns>
        public PgpPairKeyring generate()
        {
            //if ()

            return null;
        }
    }
}
