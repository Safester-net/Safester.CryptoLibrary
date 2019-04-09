using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Safester.CryptoLibrary.Samples.Src.samples
{
    public class PassphraseUtil
    {
        public static String salt = "ThiS*IsSAlt4loGin$";
        public static int PASSPHRASE_HASH_ITERATIONS = 3;

        public static String ComputeHashAndSaltedPassphrase(String login, String password)
        {
            try
            {
                SHA1 sha1 = SHA1.Create();
                // Always convert passphrase to HTML so stah the getBytes() will produce
                // the same on all platforms: Windows, Mac OS X, Linux
                String passphraseStr = HttpUtility.HtmlEncode(password);
                byte[] bArray = Encoding.UTF8.GetBytes(passphraseStr);
                byte[] bHashArray = sha1.ComputeHash(bArray);
                passphraseStr = Encoding.UTF8.GetString(bHashArray);
                if (passphraseStr.Length > 20)
                    passphraseStr = passphraseStr.Substring(0, 20);
                passphraseStr = passphraseStr.ToLower();

                //Apply salt and hash iterations
                String loginsalt = login + salt;
                //byte[] bPassphraseSaltCompute = Utils.Combine(Encoding.UTF8.GetBytes(passphraseStr), Encoding.UTF8.GetBytes(loginsalt));
                byte[] bPassphraseSaltCompute = System.Text.Encoding.UTF8.GetBytes(loginsalt);

                String connectionPassword = "";
                for (int i = 0; i < PASSPHRASE_HASH_ITERATIONS; i++)
                {
                    bPassphraseSaltCompute = sha1.ComputeHash(bPassphraseSaltCompute);
                }

                /*
                connectionPassword = Encoding.UTF8.GetString(bPassphraseSaltCompute);
                if (connectionPassword.Length > 20)
                    connectionPassword = connectionPassword.Substring(0, 20); // half of hash
                connectionPassword = connectionPassword.ToLower(); // All tests in lowercase
               */

                connectionPassword = ByteArrayToString(bPassphraseSaltCompute);
                if (connectionPassword.Length > 20)
                {
                    connectionPassword = connectionPassword.Substring(0, 20); // half of hash
                }

                return connectionPassword;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return string.Empty;
        }


        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

    }
}
