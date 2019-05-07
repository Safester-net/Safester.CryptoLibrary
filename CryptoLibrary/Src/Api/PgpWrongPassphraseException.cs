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

using Org.BouncyCastle.Bcpg.OpenPgp;
using System;

namespace Safester.CryptoLibrary.Api
{
    /// <summary>
    /// Exception thrown if a wrong passphrase is used for PGP decryption with a private and secret key.
    /// </summary>
    public class PgpWrongPassphraseException : PgpException
    {
        public PgpWrongPassphraseException()
        {
        }

        public PgpWrongPassphraseException(string message) : base(message)
        {
        }

        public PgpWrongPassphraseException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}