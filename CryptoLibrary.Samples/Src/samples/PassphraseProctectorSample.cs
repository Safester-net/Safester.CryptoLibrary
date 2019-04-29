using Safester.CryptoLibrary.Src.Api;
using System;
using System.Collections.Generic;
using System.Text;

namespace Safester.CryptoLibrary.Samples.Src.samples
{
    /// <summary>
    /// A PassphraseProtecto Sample
    /// </summary>
    public class PassphraseProctectorSample
    {
        public static void ProtectAndUnprotect()
        {
            char[] myPassphrase = "myPassphrase".ToCharArray();

            string myPassphraseProtected = PassphraseProctector.Protect(myPassphrase);
            Console.WriteLine("myPassphraseProtected: " + myPassphraseProtected);

            char[] myPassphrase2 = PassphraseProctector.Unprotect(myPassphraseProtected);
            Console.WriteLine("myPassphrase2: " + new string(myPassphrase2) + ":");

            Console.WriteLine("Press enter to close....");
            Console.ReadLine();
        }
    }
}
