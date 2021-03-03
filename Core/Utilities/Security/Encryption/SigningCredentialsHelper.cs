using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Encryption
{
    public class SigningCredentialsHelper
    {
        /* Json web tokenların oluşturulabilmesi için */
        public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
        {
            /* bir tane hashing işlemi yapacaksın anahtar olarak bu ssecurity key kullan, şifreleme olarakda gücenlik 
             * algoritmalarıdan SecurityAlgorithms.HmacSha512Signature kullan */
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
        }
    }
}
