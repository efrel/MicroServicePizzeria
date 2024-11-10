using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using Application.MSPizzeria.DTO.ViewModel.v1;

namespace Infrastructure.MSPizzeria.Service;

public class HashService
{
    // SAL ALEATORIA
    public HashResultDTO Hash(string input)
    {
        // Genera una sal aleatoria
        byte[] salt = new byte[128 / 8];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        return Hash(input, salt);
    }

    // SAL EXISTENTE
    public HashResultDTO Hash(string input, byte[] salt)
    {
        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: input,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA1,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));

        return new HashResultDTO()
        {
            Hash = hashed,
            Salt = salt
        };
    }
}