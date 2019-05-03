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
        public static readonly String VERSION_NUMBER = "v1.0.11";

        /// <summary>
        /// Release date
        /// </summary>
        public static readonly String DATE = "03-may-2019";
    }
}
