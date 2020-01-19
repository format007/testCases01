using LinkResolver.Models.Gateways.Interfaces;
using LinkResolver.Models.Tools;
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

        public SimpleCodeGenerator(byte length = 6)
        {
            this.length = length;
        }

        public string Generate()
        {
            return GenerateToken(length);
        }

        //it better to change algoritm, that doesn't based on Hex (only 16 distict values)
        private string GenerateToken(int size = 6)
        {
            var randomNumber = new byte[size/2+1];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return randomNumber.ToHexString().Substring(0,size);
            }
        }
    }
}
