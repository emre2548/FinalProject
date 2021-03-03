using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    public class AccessToken
    {
        /*  yetki gerektiren işlemlerde veri içierisine Token koyulur bu access token olur ve string olarak tutarız */
        public string Token { get; set; }
        /* Token geçerlilik süresi */
        public DateTime Expiration { get; set; }
    }
}
