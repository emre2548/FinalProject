using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public interface IDataResult<T>:IResult
    {
        /* interface interfaceyi implemente edince implemente edilen interfacedekilerde burada varmış gibi olur */

        // IResult'daki message ve success var aynı zamanda sende Data'da var dedik
        T Data { get; }
    }
}
