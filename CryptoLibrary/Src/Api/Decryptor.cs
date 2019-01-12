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

using Org.BouncyCastle.Bcpg.OpenPgp;
using Org.BouncyCastle.Utilities.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safester.CryptoLibrary.Api
{
    /// <summary>
    /// Allows to PGP decrypt a stream or a string using a private/secretkey extracted from a keyring.
    /// </summary>
    public class Decryptor
    {
        /// <summary>
        /// If true, the message integrity check passed.
        /// </summary>
        public bool Verify { get; private set; }

        private string privateArmoredKeyring = null;
        private char[] passphrase = null;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="privateArmoredKeyring">the private keyring as a Base64 armored string</param>
        /// <param name="passphrase">the passphrase to use to find the first corresponding private and secret key</param>
        public Decryptor(String privateArmoredKeyring, char[] passphrase)
        {
            if (privateArmoredKeyring == null)
            {
                throw new ArgumentNullException("privateArmoredKeyring input stream can not be null!");
            }

            if (passphrase == null)
            {
                throw new ArgumentNullException("passphrase can not be null!");
            }

            this.privateArmoredKeyring = privateArmoredKeyring;
            this.passphrase = passphrase;
        }


        /// <summary>
        /// Decrypts a input stream into an output stream. The first argument is the stream off the private keyring.
        /// </summary>
        /// <param name="inputStream">The input stream to encrypt</param>
        /// <param name="outputStream">The encrypted outout stream created by the method.</param>
        /// 
        public void Decrypt(
            Stream inputStream,
            Stream outputStream)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(privateArmoredKeyring);
            MemoryStream privateKeyring = new MemoryStream(bytes);

            if (inputStream == null)
            {
                throw new ArgumentNullException("inputStream can not be null!");
            }

            if (outputStream == null)
            {
                throw new ArgumentNullException("outputStream can not be null!");
            }

            inputStream = PgpUtilities.GetDecoderStream(inputStream);

            try
            {
                PgpObjectFactory pgpF = new PgpObjectFactory(inputStream);
                PgpEncryptedDataList enc;

                PgpObject o = pgpF.NextPgpObject();
                //
                // the first object might be a PGP marker packet.
                //
                if (o is PgpEncryptedDataList)
                {
                    enc = (PgpEncryptedDataList)o;
                }
                else
                {
                    enc = (PgpEncryptedDataList)pgpF.NextPgpObject();
                }

                //
                // find the secret key
                //
                PgpPrivateKey sKey = null;

                PgpPublicKeyEncryptedData pbe = null;
                PgpSecretKeyRingBundle pgpSec = new PgpSecretKeyRingBundle(
                    PgpUtilities.GetDecoderStream(privateKeyring));

                foreach (PgpPublicKeyEncryptedData pked in enc.GetEncryptedDataObjects())
                {
                    sKey = FindSecretKey(pgpSec, pked.KeyId, passphrase);

                    if (sKey != null)
                    {
                        pbe = pked;
                        break;
                    }
                }

                if (sKey == null)
                {
                    throw new ArgumentException("PGP Secret key for message not found.");
                }

                Stream clear = pbe.GetDataStream(sKey);

                PgpObjectFactory plainFact = new PgpObjectFactory(clear);

                PgpCompressedData cData = (PgpCompressedData)plainFact.NextPgpObject();

                PgpObjectFactory pgpFact = new PgpObjectFactory(cData.GetDataStream());

                PgpObject message = pgpFact.NextPgpObject();

                if (message is PgpLiteralData)
                {
                    PgpLiteralData ld = (PgpLiteralData)message;

                    /*
					string outFileName = ld.FileName;
                    if (outFileName.Length == 0)
					{
						outFileName = defaultFileName;
					}
                    */

                    //string outFileName = defaultFileName;
                    //Stream fOut = File.Create(outFileName);

                    Stream unc = ld.GetInputStream();
                    Streams.PipeAll(unc, outputStream);
                }
                else if (message is PgpOnePassSignatureList)
                {
                    throw new PgpException("Encrypted message contains a signed message - not literal data.");
                }
                else
                {
                    throw new PgpException("Message is not a simple encrypted file - type unknown.");
                }

                if (pbe.IsIntegrityProtected())
                {
                    if (!pbe.Verify())
                    {
                       Verify = false;
                    }
                    else
                    {
                        Verify = true;
                    }
                }
                else
                {
                    //Don't know what to doe
                    //Console.Error.WriteLine("no message integrity check");
                }
            }
            catch (PgpException e)
            {
                /*
                Console.Error.WriteLine(e);

                Exception underlyingException = e.InnerException;
                if (underlyingException != null)
                {
                    Console.Error.WriteLine(underlyingException.Message);
                    Console.Error.WriteLine(underlyingException.StackTrace);
                }
                */

                if (e.Message.Contains("Checksum mismatch at 0 of 20"))
                {
                    throw new PgpWrongPassphraseException("Wrong passphrase. Can not decrypt.");
                }

                throw e;
            }
            finally
            {
                inputStream.Dispose();

                //Because stream mut remain open fro string decryption
                if (outputStream.GetType() != typeof(MemoryStream))
                {
                    outputStream.Dispose(); // NO: to be done by caller.
                }
                    
            }
        }

        /// <summary>
        /// Decrypts a input stream into an output stream. The first argument is the stream off the private keyring.
        /// </summary>
        /// <param name="inText">The text to decrypt</param>
        /// 
        /// 
        /// <returns>The decrypted text</returns>
        public string Decrypt(string inText)
        {
            if (inText == null)
            {
                throw new ArgumentNullException("inText can not be null!");
            }

            byte[] bytes = Encoding.UTF8.GetBytes(inText);
            MemoryStream inMemoryStream = new MemoryStream(bytes);
            MemoryStream outMemoryStream = new MemoryStream();

            Decrypt(inMemoryStream, outMemoryStream);
            string result = Encoding.UTF8.GetString(outMemoryStream.ToArray(), 0, (int)outMemoryStream.Length);
            return result;
        }

        /**
         * Search a secret key ring collection for a secret key corresponding to
         * keyId if it exists.
         *
         * @param pgpSec a secret key ring collection.
         * @param keyId keyId we want.
         * @param passphrase passphrase to decrypt secret key with.
         * @return
         */
        private static PgpPrivateKey FindSecretKey(
            PgpSecretKeyRingBundle pgpSec,
            long keyId,
            char[] passphrase)
        {
            PgpSecretKey pgpSecKey = pgpSec.GetSecretKey(keyId);

            if (pgpSecKey == null)
            {
                return null;
            }

            return pgpSecKey.ExtractPrivateKey(passphrase);
        }
    }
}
