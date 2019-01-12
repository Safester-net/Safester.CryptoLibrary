using Safester.CryptoLibrary.Samples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoLibrary.Samples.Src
{
    public class MainControler
    {
        static int classToRun = 2;

        public static void Main(string[] args)
        {
            if (classToRun == 1)
            {
                FileEncryptionSample.MainFileEncrytion();
            }
            else if (classToRun == 2) { 

                PgpKeyPairGeneratorSample pgpKeyPairGeneratorSample = new PgpKeyPairGeneratorSample();
                pgpKeyPairGeneratorSample.Generate();
            }
        }
    }
}
