/* 
 * This file is part of Safester C# OpenPGP SDK.                                
 * Copyright(C) 2019,  KawanSoft SAS
 * (http://www.kawansoft.com). All rights reserved.                                
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License. 
 */

using Org.BouncyCastle.Bcpg;
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
            SecureRandom secureRandom = PgpEncryptionUtil.GetSecureRandom();

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
