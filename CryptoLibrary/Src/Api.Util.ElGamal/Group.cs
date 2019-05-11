using Org.BouncyCastle.Math;
using System;
using System.Collections.Generic;
using System.Text;

namespace Safester.CryptoLibrary.Src.Api
{
    public class Group
    {
        private BigInteger
    p,
    q,
    g;

        /*package*/
        public Group(BigInteger p, BigInteger q, BigInteger g)
        {
            this.p = p;
            this.q = q;
            this.g = g;
        }


        public BigInteger GetP()
        {
            return this.p;
        }


        public BigInteger GetQ()
        {
            return this.q;
        }


        public BigInteger GetG()
        {
            return this.g;
        }

    }
}
