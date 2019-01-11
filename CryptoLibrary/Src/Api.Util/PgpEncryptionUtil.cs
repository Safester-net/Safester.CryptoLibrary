using Org.BouncyCastle.Bcpg.OpenPgp;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safester.CryptoLibrary.Api
{
    class PgpEncryptionUtil
    {
        /// <summary>Write out the passed in file as a literal data packet in partial packet format.</summary>
        public static void WriteFileToLiteralData(
            Stream inputStream,
            Stream outputStream,
            char fileType,
            string fileName,
            byte[] buffer)
        {

            if (fileName == null)
            {
                fileName = "fileName";
            }

            try
            {

                PgpLiteralDataGenerator lData = new PgpLiteralDataGenerator();
                Stream pOut = lData.Open(outputStream, fileType, fileName, DateTime.Now, buffer);
                byte[] buf = new byte[buffer.Length];

                int len;
                while ((len = inputStream.Read(buf, 0, buf.Length)) > 0)
                {
                    pOut.Write(buf, 0, len);
                }

                lData.Close();
            }
            finally
            {
                inputStream.Dispose();
            }

        }
    }
}
