using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    // Temel voidler için başlangıç
    public interface IResult
    {
        /* içerisinde bir tane işlem sonucu birtanede kullanıcıyı bilgillendirmek adına mesaj olacak. 
         * uygulamayı kullancak olan kişileri ve API'leri doğru yönlendirme yapmak amaç */


        bool Success { get; }  // only read

        string Message { get; }

    }
}
