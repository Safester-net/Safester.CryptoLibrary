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
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.Security;
using Safester.CryptoLibrary.Api.Util;

namespace Safester.CryptoLibrary.Api
{
    /// <summary>
    /// Allows to PGP encrypt a stream or a string using a List of PGP Public Keys (PgpPublicKey objects).
    /// </summary>
    public class PgpEncryptor
    {

        private bool armor = false;
        private bool withIntegrityCheck = true;

        /// <summary>
        /// if true resulating file will be be Base64 armored
        /// </summary>
        public bool Armor { get => armor; }

        /// <summary>
        /// If true, an integrity check is done. It can be tested at decryption. 
        /// </summary>
        public bool WithIntegrityCheck { get => withIntegrityCheck; }

        /// <summary>
        /// Default constructor. Encrypts with no armor and with integerity check.
        /// </summary>
        public PgpEncryptor()
        {

        }

        
        /// <summary>
        /// Constructor that allows to define if encryption is armored and integrity check done.
        /// </summary>
        /// <param name="armor">if true, the reuslting file will be be Base64 armored.</param>
        /// <param name="withIntegrityCheck">If true an integrity check will be done</param>
        public PgpEncryptor(bool armor, bool withIntegrityCheck)
        {
            this.armor = armor;
            this.withIntegrityCheck = withIntegrityCheck;
        }


 

        /// <summary>
        /// PGP Encrypts an input stream for a list of PgpPublicKey a stream.
        /// </summary>
        /// <param name="pgpPublicKeys">The List of PgpPublicKey to encrypt for</param>
        /// <param name="inputStream">The input stream of the file or text to encrypt</param>
        /// <param name="outputStream">The encrypted output stream created by the method</param>
        public void Encrypt(
              List<PgpPublicKey> pgpPublicKeys,
              Stream inputStream,
              Stream outputStream)
        {

            if (pgpPublicKeys == null)
            {
                throw new ArgumentNullException("pgpPublicKeys List can not be null!"); 
            }

            if (pgpPublicKeys.Count == 0)
            {
                throw new ArgumentException("pgpPublicKeys List does not contain any PgpPublicKey!");
            }

            if (inputStream == null)
            {
                throw new ArgumentNullException("inputStream can not be null!");
            }

            if (outputStream == null)
            {
                throw new ArgumentNullException("outputStream can not be null!");
            }

            if (Armor)
            {
                outputStream = new ArmoredOutputStream(outputStream);
            }

            try
            {
                PgpEncryptedDataGenerator cPk = new PgpEncryptedDataGenerator(SymmetricKeyAlgorithmTag.Cast5, WithIntegrityCheck, new SecureRandom());

                foreach (PgpPublicKey encKey in pgpPublicKeys)
                {
                    cPk.AddMethod(encKey);
                }

                Stream cOut = null;

                cOut = cPk.Open(outputStream, new byte[1 << 16]);

                try
                {
                    PgpCompressedDataGenerator comData = new PgpCompressedDataGenerator(
    CompressionAlgorithmTag.Zip);

                    PgpEncryptionUtil.WriteFileToLiteralData(
                        inputStream,
                        comData.Open(cOut),
                        PgpLiteralData.Binary,
                        null,
                        new byte[1 << 16]);

                    comData.Close();

                }
                finally
                {
                    cOut.Dispose();

                    if (Armor)
                    {
                        outputStream.Dispose();
                    }
                }


            }
            catch (PgpException e)
            {
                /*
                //Console.Error.WriteLine(e);

                Exception underlyingException = e.InnerException;
                if (underlyingException != null)
                {
                    //Console.Error.WriteLine(underlyingException.Message);
                    //Console.Error.WriteLine(underlyingException.StackTrace);
                }
                */

                throw e;
            }
        }

        /// <summary>
        /// PGP Encrypts a text that will be armored.
        /// </summary>
        /// <param name="pgpPublicKeys"></param>
        /// <param name="inText"></param>
        /// <returns>the encrypted text (will always be armored always armored)</returns>
        public string Encrypt(List<PgpPublicKey> pgpPublicKeys, string inText)
        {
            if (pgpPublicKeys == null)
            {
                throw new ArgumentNullException("pgpPublicKeys List can not be null!");
            }

            if (pgpPublicKeys.Count == 0)
            {
                throw new ArgumentException("pgpPublicKeys List does not contain any PgpPublicKey!");
            }

            if (inText == null)
            {
                throw new ArgumentException("inText can not be null!");
            }

            byte[] bytes = Encoding.UTF8.GetBytes(inText);
            MemoryStream inMemoryStream = new MemoryStream(bytes);
            MemoryStream outMemoryStream = new MemoryStream();

            bool prevArmor = this.Armor;
            this.armor = true;
            Encrypt(pgpPublicKeys, inMemoryStream, outMemoryStream);
            string result = Encoding.UTF8.GetString(outMemoryStream.ToArray(), 0, (int)outMemoryStream.Length);
            this.armor = prevArmor;
            return result;

        }
    }
}
