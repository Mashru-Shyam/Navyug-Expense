using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Generators
{
    public static class EmailTokenGenerator
    {
        public static string Generate()
        {
            return Convert.ToBase64String(
                RandomNumberGenerator.GetBytes(48));
        }
    }
}
