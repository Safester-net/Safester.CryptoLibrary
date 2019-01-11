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
    public class PgpDecryptor
    {
        /// <summary>
        /// If true, the message integrity check passed.
        /// </summary>
        public bool Verify { get; private set; }

        /// <summary>
        /// Decrypts a input stream into an output stream. The first argument is the stream off the private keyring.
        /// </summary>
        /// <param name="privateKeyring">the stream off the private keyring</param>
        /// <param name="passphrase">the passphrase to use to find the first corresponding private & secret key</param>
        /// <param name="inputStream">The input stream to encrypt</param>
        /// <param name="outputStream">The encrypted outout stream created by the method.</param>
        /// 
        public void Decrypt(
            Stream privateKeyring,
            char[] passphrase,
            Stream inputStream,
            Stream outputStream)
        {
            if (privateKeyring == null)
            {
                throw new ArgumentNullException("privateKeyring input stream is null!");
            }

            if (passphrase == null)
            {
                throw new ArgumentNullException("passphrase is null!");
            }

            if (inputStream == null)
            {
                throw new ArgumentNullException("inputStream is null!");
            }

            if (outputStream == null)
            {
                throw new ArgumentNullException("outputStream is null!");
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
                    throw new PgpExceptionWrongPassphrase("Wrong passphrase. Can not decrypt.");
                }

                throw e;
            }
            finally
            {
                inputStream.Dispose();
                //outputStream.Dispose(); // NO: to be done by caller.
            }
        }

        /// <summary>
        /// Decrypts a input stream into an output stream. The first argument is the stream off the private keyring.
        /// </summary>
        /// <param name="privateKeyring">the stream off the private keyring.</param>
        /// <param name="passphrase">the passphrase to use to find the first corresponding private & secret key</param>
        /// <param name="inText">The text ro decrypt</param>
        /// <returns>The decrypted text</returns>
        public string Decrypt(Stream privateKeyring, char[] passphrase, string inText)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(inText);
            MemoryStream inMemoryStream = new MemoryStream(bytes);
            MemoryStream outMemoryStream = new MemoryStream();

            Decrypt(privateKeyring, passphrase, inMemoryStream, outMemoryStream);
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
