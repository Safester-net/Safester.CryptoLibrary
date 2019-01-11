using Org.BouncyCastle.Bcpg.OpenPgp;
using System;

namespace Safester.CryptoLibrary.Api
{
    /// <summary>
    /// Exception thrown if a wrong passphrase is used for PGP decryption with a private & secret key.
    /// </summary>
    public class PgpExceptionWrongPassphrase : PgpException
    {
        public PgpExceptionWrongPassphrase()
        {
        }

        public PgpExceptionWrongPassphrase(string message) : base(message)
        {
        }

        public PgpExceptionWrongPassphrase(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}