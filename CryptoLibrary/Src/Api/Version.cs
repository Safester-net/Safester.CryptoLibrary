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

using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 10/01/19 21:21 NDP: All encryptions done
// 11/01/19 15:18 NDP: All key generation done
// 12/01/19 09:12 NDP: Clean key pair generation
// 12/01/19 09:51 NDP: Clean all APIs
// 12/01/19 11:03 NDP: Rewrite MainControler
// 12/01/19 18:04 NDP: Private keyring and passephrase is passed to Decryptor constructor
// 12/01/19 19:48 NDP: Clean classes & method names
// 12/01/19 19:48 NDP: Rename classes & method names
// 12/01/19 20:18 NDP: Comments & Regenerate Nuget 
// 13/01/19 00:29 NDP: No more stream keyring in Decryptor
// 13/01/19 10:53 NDP: Update project labels

namespace Safester.CryptoLibrary.Api
{
    /// <summary>
    /// Version info.
    /// </summary>
    public class Version
    {
        /// <summary>
        /// Product name
        /// </summary>
        public static readonly String PRODUCT = "Safester OpenPGP Client SDK";

        /// <summary>
        /// Version number.
        /// </summary>
        public static readonly String VERSION_NUMBER = "v1.0.4";

        /// <summary>
        /// Release date
        /// </summary>
        public static readonly String DATE = "13-jan-2019";
    }
}
