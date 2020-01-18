using LinkResolver.Models.Gateways.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace LinkResolver.Models.Gateways
{
    public class SimpleCodeGenerator : ICodeGenerator
    {
        private byte length;

        public SimpleCodeGenerator(byte length = 16)
        {
            this.length = length;
        }

        public byte[] Generate()
        {
            return GenerateToken(length);
        }

        private byte[] GenerateToken(int size = 6)
        {
            var randomNumber = new byte[size];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return randomNumber;
            }
        }
    }
}
