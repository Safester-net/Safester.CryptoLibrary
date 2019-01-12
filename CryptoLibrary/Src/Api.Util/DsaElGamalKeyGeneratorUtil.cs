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

            // Prepare a strong Secure Random with seed
            SecureRandom secureRandom = PgpEncryptionUtil.getSecureRandom();

            PgpKeyRingGenerator keyRingGen = new PgpKeyRingGenerator(PgpSignature.PositiveCertification, dsaKeyPair,
                identity, SymmetricKeyAlgorithmTag.Aes256, passPhrase, true, null, null, secureRandom);

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
