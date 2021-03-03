using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    public interface ITokenHelper
    {
        /* kullanıcı bilgilerini girdi ve API kısmına bilgilerini yolladı eğer bilgiler doğruysa CreateToken çalışacak
         * ilgili kullanıcı için DB gidecek Claim leri bulacak orada birtane bu bilgileri barındıran Jason Web Token oluşturucak ve bilgieri geri gönderecek */
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims);

    }
}
