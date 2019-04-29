using System;
using System.Collections.Generic;
using System.Text;

namespace Safester.CryptoLibrary.Src.Api.Util
{
    /// <summary>
    /// A sample tha shows how to use HexConverter.
    /// </summary>
    public class HexConverterSample
    {
        /// <summary>
        /// Displays on console a convert to anf from hex.
        /// </summary>
        public static void Convert()
        {
            char[] myPassphrase = "myPassphrase".ToCharArray();
            byte[] bArray = Encoding.UTF8.GetBytes(new String(myPassphrase));
            string hex = HexConverter.ToHexString(bArray);
            Console.WriteLine("hex      : " + hex);

            byte[] byteArrayback = HexConverter.ToByteArray(hex);
            string utfString = Encoding.UTF8.GetString(byteArrayback, 0, byteArrayback.Length);
            Console.WriteLine("utfString: " + utfString + ":");
            Console.WriteLine();

            Console.WriteLine("Press enter to close....");
            Console.ReadLine();
        }

    }
}
