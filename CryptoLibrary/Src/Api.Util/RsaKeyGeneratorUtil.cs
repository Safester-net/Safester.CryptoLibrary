﻿using Org.BouncyCastle.Bcpg;
using Org.BouncyCastle.Bcpg.OpenPgp;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using Safester.CryptoLibrary.Api.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safester.CryptoLibrary.Src.Api.Util
{
    /// <summary>
    /// Util class for RSA key pair generator.
    /// </summary>
    public class RsaKeyGeneratorUtil
    {
        /// <summary>
        /// Final key pair creation
        /// </summary>
        /// <param name="secretOut"></param>
        /// <param name="publicOut"></param>
        /// <param name="publicKey"></param>
        /// <param name="privateKey"></param>
        /// <param name="identity"></param>
        /// <param name="passphrase"></param>
        /// <param name="armor"></param>
        public static void ExportKeyPair(
         Stream secretOut,
         Stream publicOut,
         AsymmetricKeyParameter publicKey,
         AsymmetricKeyParameter privateKey,
         string identity,
         char[] passphrase,
         bool armor)
        {
            if (armor)
            {
                secretOut = new ArmoredOutputStream(secretOut);
            }

            // Prepare a strong Secure Random with seed
            SecureRandom secureRandom = PgpEncryptionUtil.getSecureRandom();

            PgpSecretKey secretKey = new PgpSecretKey(
                PgpSignature.DefaultCertification,
                PublicKeyAlgorithmTag.RsaGeneral,
                publicKey,
                privateKey,
                DateTime.UtcNow,
                identity,
                SymmetricKeyAlgorithmTag.Aes256,
                passphrase,
                null,
                null,
                secureRandom
                );

            secretKey.Encode(secretOut);

            if (armor)
            {
                secretOut.Dispose();
                publicOut = new ArmoredOutputStream(publicOut);
            }

            PgpPublicKey key = secretKey.PublicKey;

            key.Encode(publicOut);

            if (armor)
            {
                publicOut.Dispose();
            }
        }

    }
}
