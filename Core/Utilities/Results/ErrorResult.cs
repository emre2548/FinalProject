using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class ErrorResult:Result
    {
        public ErrorResult(string message) : base(false, message)
        {

        }

        // success true bilgisini buradan yolluyoruz
        public ErrorResult() : base(false)
        {

        }
    }
}
