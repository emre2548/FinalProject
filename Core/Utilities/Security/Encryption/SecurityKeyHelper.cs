using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Encryption
{
    public class SecurityKeyHelper
    {
        /*  "SecurityKey": "mysuperscretkeymysuperscretkey" oluşturuan keyi bunları asp net jason web token servislerinin anlauacağı hale getiriyoruz */
        public static SecurityKey CreateSecurityKey(string securityKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }
    }
}
