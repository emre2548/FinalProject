using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Hashing
{
    /* static class olduğu için çıplak kalabilir */
    public class HashingHelper
    {
        /* out veriyi dışarı verecek */
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {/* şifrein hash ve salt oluşturacak */
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            /* burada hesaplanan hesh salt kullanılarak yapıalcak yani anahtar salt oluyor */
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                /* değerleri aynı mı */
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
