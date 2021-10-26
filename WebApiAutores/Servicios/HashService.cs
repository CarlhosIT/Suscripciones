using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using WebApiAutores.DTOs;

namespace WebApiAutores.Servicios
{
    public class HashService
    {

        public ResultadoHashDTO Hash(String TextoPlano) 
        {
            var Sal = new byte[16];
            using (var random = RandomNumberGenerator.Create()) 
            {
                random.GetBytes(Sal);
            }

            return Hash(TextoPlano, Sal);
        }

        public ResultadoHashDTO Hash(String textoPlano,byte[] sal) 
        {
            var LlaveDerivada = KeyDerivation.Pbkdf2(
                    password:textoPlano,salt:sal,KeyDerivationPrf.HMACSHA1,iterationCount:10000,numBytesRequested:32);

            var hash = Convert.ToBase64String(LlaveDerivada);

            return new ResultadoHashDTO { Hash=hash,Sal=sal};

        }

    }
}
