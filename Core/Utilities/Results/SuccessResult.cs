using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class SuccessResult:Result
    {
        // base Result demek oraya gönderiyoruz
        public SuccessResult(string message) : base(true, message)
        {

        }

        // success true bilgisini buradan yolluyoruz
        public SuccessResult() : base(true)
        {

        }
    }
}
