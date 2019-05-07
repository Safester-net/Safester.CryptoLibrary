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

using CryptoLibrary.Samples;
using CryptoLibrary.Samples.Src;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safester.CryptoLibrary.Samples.Src.tests
{
    public class TestSafesterKeysAndFiles
    {
        public static void Test()
        {
            // For safester tess
            string encryptedFileSafester = MainControler.basePath + "\\1182-d181213d-1544682969189_comptes.xlsx.pgp";
            string defaultPrivateKeyring = MainControler.basePath + "\\priv_key.txt";
            //string defaultPublicKeyring = MainControler.basePath + "\\pub_key.txt";

            Console.WriteLine(DateTime.Now + " File Decryption...");
            string privateKeyringSafester = File.ReadAllText(defaultPrivateKeyring);

            FileDecryptionSample fileDecryptionSampleSafester = new FileDecryptionSample(privateKeyringSafester, MainControler.passphrase.ToCharArray());
            string decryptedFileSafester = encryptedFileSafester.Substring(0, encryptedFileSafester.LastIndexOf("."));
            fileDecryptionSampleSafester.Decrypt(encryptedFileSafester, decryptedFileSafester);

            Console.WriteLine();
            Console.WriteLine("Press enter to close....");
            Console.ReadLine();
        }
    }
}
