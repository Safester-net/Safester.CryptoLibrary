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

namespace Safester.CryptoLibrary.Api
{
    /// <summary>
    /// Available PGP public key  algorithms key lengths.
    /// </summary>
    public enum PublicKeyLength
    {
        BITS_1024 = 1024,
        BITS_2048 = 2048,
        BITS_3072 = 3072,
        BITS_4096 = 4096,
    }
}