using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {

        public Result(bool success, string message):this(success)
        {
            Message = message; // aşağıdaki MEssage'yi message olarak set et read only'ler ccontractor dışında set edilebilir.
                               //Success = success; 
            /* success işlemini aşağıya veriyoruz bu satırı siliyoruz kendini tekrar etme prensibinden dolayı bu kod çalışınca aşağısıda çalışacak 
             * :this kensini ifade diyor ve :this(success) yazarsak iki parametre gönderen birisi için iki fonksiyonda çalıacak sadece success
             * gönderen biris için ise success çalışacak */
        }

        public Result(bool success)
        {
            Success = success;
        }

        // sadece get var ne yazarsak onu return eder
        //public bool Success => throw new NotImplementedException();
        public bool Success { get; }

        public string Message { get; }

    }
}
