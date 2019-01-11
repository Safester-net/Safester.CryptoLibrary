using Org.BouncyCastle.Bcpg;
using Org.BouncyCastle.Bcpg.OpenPgp;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safester.CryptoLibrary.Src.Api.Util
{
    public class DsaElGamalKeyGeneratorUtil
    {

        public static void ExportKeyPair(
            Stream secretOut,
            Stream publicOut,
            AsymmetricCipherKeyPair dsaKp,
            AsymmetricCipherKeyPair elgKp,
            string identity,
            char[] passPhrase,
            bool armor)
        {
            if (armor)
            {
                secretOut = new ArmoredOutputStream(secretOut);
            }

            PgpKeyPair dsaKeyPair = new PgpKeyPair(PublicKeyAlgorithmTag.Dsa, dsaKp, DateTime.UtcNow);
            PgpKeyPair elgKeyPair = new PgpKeyPair(PublicKeyAlgorithmTag.ElGamalEncrypt, elgKp, DateTime.UtcNow);

            PgpKeyRingGenerator keyRingGen = new PgpKeyRingGenerator(PgpSignature.PositiveCertification, dsaKeyPair,
                identity, SymmetricKeyAlgorithmTag.Aes256, passPhrase, true, null, null, new SecureRandom());

            keyRingGen.AddSubKey(elgKeyPair);

            keyRingGen.GenerateSecretKeyRing().Encode(secretOut);

            if (armor)
            {
                secretOut.Dispose();
                publicOut = new ArmoredOutputStream(publicOut);
            }

            keyRingGen.GeneratePublicKeyRing().Encode(publicOut);

            if (armor)
            {
                publicOut.Dispose();
            }
        }
    }
}
