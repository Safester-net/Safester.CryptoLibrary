/* 
 * This file is part of Safester C# OpenPGP SDK.                                
 * Copyright(C) 2019,  KawanSoft SAS
 * (https://www.safester.net). All rights reserved.                                
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
    /// Allows to generate a PGP Key Pair. 
    /// Will create a string Base64 armored keyring for both the
    /// private and public PGP key.
    /// </summary>
    public class PgpKeyPairGenerator
    {

        /// Member values 
        private string identity = null;
        private char[] passphrase = null;
        private PublicKeyAlgorithm publicKeyAlgorithm = PublicKeyAlgorithm.DSA_ELGAMAL;
        private PublicKeyLength publicKeyLength = PublicKeyLength.BITS_1024;


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="identity">Email of key pair owner</param>
        /// <param name="passphrase">Passphrase of key pair</param>
        /// <param name="publicKeyAlgorithm">PublicKeyAlgorithm.DSA_ELGAMAL or PublicKeyAlgorithm.RSA</param>
        /// <param name="publicKeyLength">PublicKeyLength.BITS_1024, PublicKeyLength.BITS_2048, PublicKeyLength.BITS_3072,,</param>
        public PgpKeyPairGenerator(string identity, char[] passphrase, PublicKeyAlgorithm publicKeyAlgorithm, PublicKeyLength publicKeyLength) 
        {
            if (identity == null)
            {
                throw new ArgumentNullException("identity can not be null!");
            }

            if (passphrase == null)
            {
                throw new ArgumentNullException("passphrase can not be null!");
            }

            if (publicKeyAlgorithm == PublicKeyAlgorithm.DSA_ELGAMAL && publicKeyLength > PublicKeyLength.BITS_1024 )
            {
                throw new InvalidParameterException("Key length must be <= 1024 bits with DSA/ELGAMAL algorithm.");
            }

            this.identity = identity;
            this.passphrase = passphrase;
            this.publicKeyAlgorithm = publicKeyAlgorithm;
            this.publicKeyLength = publicKeyLength;


        }

        /// <summary>
        /// Generates the key pair on keyring on output streams. Streams are closed my method.
        /// </summary>
        /// <param name="outSecret">the private/secret key keyring output stream</param>
        /// <param name="outPublic">the public key keyring output stream</param>
        public void Generate(Stream outSecret, Stream outPublic)
        {
            if (outSecret == null)
            {
                throw new ArgumentNullException("outSecret can not be null!");
            }

            if (outPublic == null)
            {
                throw new ArgumentNullException("outPublic can not be null!");
            }

            try
            {
                if (publicKeyAlgorithm == PublicKeyAlgorithm.RSA)
                {
                    GenerateRsa(outSecret, outPublic);
                }
                else
                {
                    GenerateElGamal(outSecret, outPublic);
                }
            }
            finally
            {
                outSecret.Dispose();
                outPublic.Dispose();
            }
        }


        /// <summary>
        /// Generates the armored private and public keyrings. 
        /// </summary>
        /// <returns>The PgpKeyPairHolder that contains armored private/secret keyring and armored public keyring </returns>
        public PgpKeyPairHolder Generate()
        {
            MemoryStream outSecret = new MemoryStream();
            MemoryStream outPublic = new MemoryStream();

            if (publicKeyAlgorithm == PublicKeyAlgorithm.RSA)
            {
                GenerateRsa(outSecret, outPublic);
            }
            else
            {
                GenerateElGamal(outSecret, outPublic);
            }

            string secretKeyRing = Encoding.UTF8.GetString(outSecret.ToArray(), 0, (int)outSecret.Length);
            string publicKeyRing = Encoding.UTF8.GetString(outPublic.ToArray(), 0, (int)outPublic.Length);
            PgpKeyPairHolder pgpKeyPairHolder = new PgpKeyPairHolder(secretKeyRing, publicKeyRing);
            return pgpKeyPairHolder;
        }

        private void GenerateRsa(Stream outSecret, Stream outPublic)
        {
            IAsymmetricCipherKeyPairGenerator kpg = GeneratorUtilities.GetKeyPairGenerator("RSA");

            // Prepare a strong Secure Random with seed
            SecureRandom secureRandom = PgpEncryptionUtil.GetSecureRandom();

            kpg.Init(new RsaKeyGenerationParameters(
                Org.BouncyCastle.Math.BigInteger.ValueOf(0x10001), secureRandom, (int) publicKeyLength, 25));

            AsymmetricCipherKeyPair kp = kpg.GenerateKeyPair();
            RsaKeyGeneratorUtil.ExportKeyPair(outSecret, outPublic, kp.Public, kp.Private, identity, passphrase, true);
        }

        private void GenerateElGamal(Stream outSecret, Stream outPublic)
        {
            // Prepare a strong Secure Random with seed
            SecureRandom secureRandom = PgpEncryptionUtil.GetSecureRandom();

            IAsymmetricCipherKeyPairGenerator dsaKpg = GeneratorUtilities.GetKeyPairGenerator("DSA");
            DsaParametersGenerator pGen = new DsaParametersGenerator();
            pGen.Init((int)publicKeyLength, 80, new SecureRandom());
            DsaParameters dsaParams = pGen.GenerateParameters();
            DsaKeyGenerationParameters kgp = new DsaKeyGenerationParameters(secureRandom, dsaParams);
            dsaKpg.Init(kgp);

            //
            // this takes a while as the key generator has to Generate some DSA parameters
            // before it Generates the key.
            //
            AsymmetricCipherKeyPair dsaKp = dsaKpg.GenerateKeyPair();
            IAsymmetricCipherKeyPairGenerator elgKpg = GeneratorUtilities.GetKeyPairGenerator("ELGAMAL");

            Org.BouncyCastle.Math.BigInteger g = new Org.BouncyCastle.Math.BigInteger("153d5d6172adb43045b68ae8e1de1070b6137005686d29d3d73a7749199681ee5b212c9b96bfdcfa5b20cd5e3fd2044895d609cf9b410b7a0f12ca1cb9a428cc", 16);
            Org.BouncyCastle.Math.BigInteger p = new Org.BouncyCastle.Math.BigInteger("9494fec095f3b85ee286542b3836fc81a5dd0a0349b4c239dd38744d488cf8e31db8bcb7d33b41abb9e5a33cca9144b1cef332c94bf0573bf047a3aca98cdf3b", 16);

            secureRandom = PgpEncryptionUtil.GetSecureRandom();
            ElGamalParameters elParams = new ElGamalParameters(p, g);
            ElGamalKeyGenerationParameters elKgp = new ElGamalKeyGenerationParameters(secureRandom, elParams);
            elgKpg.Init(elKgp);

            //
            // this is quicker because we are using preGenerated parameters.
            //
            AsymmetricCipherKeyPair elgKp = elgKpg.GenerateKeyPair();
            DsaElGamalKeyGeneratorUtil.ExportKeyPair(outSecret, outPublic, dsaKp, elgKp, identity, passphrase, true);
        }

    }
}
