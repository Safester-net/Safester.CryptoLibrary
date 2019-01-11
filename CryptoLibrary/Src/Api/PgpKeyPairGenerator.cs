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
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Bcpg.OpenPgp;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.Crypto.Generators;
using Safester.CryptoLibrary.Src.Api.Util;
using Safester.CryptoLibrary.Api.Util;

namespace Safester.CryptoLibrary.Api
{
    /// <summary>
    /// Allows to generate a PGP Key Pair that will be use for Register. 
    /// Will create a strign armored keyring for both the
    /// private and public PGP key.
    /// </summary>
    public class PgpKeyPairGenerator
    {

        /// Updatable values 
        private PgpAsymAlgo pgpAsymAlgo = PgpAsymAlgo.DSA_ELGAMAL;
        private PgpAsymKeyLength pgpAsymKeyLength = PgpAsymKeyLength.LENGTH_2048;

        private string identity = null;
        private char[] passphrase = null;


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
        /// <returns>The PgpPairKeyring that contains arrmored private/secret keyring & armored public keyring </returns>
        public PgpPairKeyring generate()
        {
            PgpPairKeyring pgpPairKeyring = null;
            if (pgpAsymAlgo == PgpAsymAlgo.RSA)
            {
                pgpPairKeyring = generateRsa();
            }
            else
            {
                pgpPairKeyring = generateElGamal();
            }

            return pgpPairKeyring;
        }

        private PgpPairKeyring generateRsa()
        {
            IAsymmetricCipherKeyPairGenerator kpg = GeneratorUtilities.GetKeyPairGenerator("RSA");

            // Prepare a strong Secure Random with seed
            SecureRandom secureRandom = PgpEncryptionUtil.getSecureRandom();

            kpg.Init(new RsaKeyGenerationParameters(
                Org.BouncyCastle.Math.BigInteger.ValueOf(0x10001), secureRandom, (int) pgpAsymKeyLength, 25));

            AsymmetricCipherKeyPair kp = kpg.GenerateKeyPair();

            MemoryStream outSecret = new MemoryStream();
            MemoryStream outPublic = new MemoryStream();

            RsaKeyGeneratorUtil.ExportKeyPair(outSecret, outPublic, kp.Public, kp.Private, identity, passphrase, true);
            string secretKeyRing = Encoding.UTF8.GetString(outSecret.ToArray(), 0, (int)outSecret.Length);
            string publicKeyRing = Encoding.UTF8.GetString(outPublic.ToArray(), 0, (int)outPublic.Length);
            PgpPairKeyring pgpPairKeyring = new PgpPairKeyring(secretKeyRing, publicKeyRing);
            return pgpPairKeyring;
        }

        private PgpPairKeyring generateElGamal()
        {
            // Prepare a strong Secure Random with seed
            // SecureRandom secureRandom = getSecureRandom();

            IAsymmetricCipherKeyPairGenerator dsaKpg = GeneratorUtilities.GetKeyPairGenerator("DSA");
            DsaParametersGenerator pGen = new DsaParametersGenerator();
            pGen.Init(1024, 80, new SecureRandom());
            DsaParameters dsaParams = pGen.GenerateParameters();
            DsaKeyGenerationParameters kgp = new DsaKeyGenerationParameters(new SecureRandom(), dsaParams);
            dsaKpg.Init(kgp);

            //
            // this takes a while as the key generator has to Generate some DSA parameters
            // before it Generates the key.
            //
            AsymmetricCipherKeyPair dsaKp = dsaKpg.GenerateKeyPair();

            IAsymmetricCipherKeyPairGenerator elgKpg = GeneratorUtilities.GetKeyPairGenerator("ELGAMAL");

            Org.BouncyCastle.Math.BigInteger g = new Org.BouncyCastle.Math.BigInteger("153d5d6172adb43045b68ae8e1de1070b6137005686d29d3d73a7749199681ee5b212c9b96bfdcfa5b20cd5e3fd2044895d609cf9b410b7a0f12ca1cb9a428cc", 16);
            Org.BouncyCastle.Math.BigInteger p = new Org.BouncyCastle.Math.BigInteger("9494fec095f3b85ee286542b3836fc81a5dd0a0349b4c239dd38744d488cf8e31db8bcb7d33b41abb9e5a33cca9144b1cef332c94bf0573bf047a3aca98cdf3b", 16);

            ElGamalParameters elParams = new ElGamalParameters(p, g);
            ElGamalKeyGenerationParameters elKgp = new ElGamalKeyGenerationParameters(new SecureRandom(), elParams);
            elgKpg.Init(elKgp);

            //
            // this is quicker because we are using preGenerated parameters.
            //
            AsymmetricCipherKeyPair elgKp = elgKpg.GenerateKeyPair();

            MemoryStream outSecret = new MemoryStream();
            MemoryStream outPublic = new MemoryStream();

            DsaElGamalKeyGeneratorUtil.ExportKeyPair(outSecret, outPublic, dsaKp, elgKp, identity, passphrase, true);

            string secretKeyRing = Encoding.UTF8.GetString(outSecret.ToArray(), 0, (int)outSecret.Length);
            string publicKeyRing = Encoding.UTF8.GetString(outPublic.ToArray(), 0, (int)outPublic.Length);
            PgpPairKeyring pgpPairKeyring = new PgpPairKeyring(secretKeyRing, publicKeyRing);
            return pgpPairKeyring;

        }


    }
}
