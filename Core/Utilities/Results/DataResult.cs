using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class DataResult<T> : Result, IDataResult<T>
    {
        // constractor ekliyoruz ctor tab tab
        public DataResult(T data, bool success, string message):base(success,message) // diğer resulttan farkı datası var ve base'de success ve message yolla 
        {
            Data = data;
        }

        public DataResult(T data, bool success):base(success)
        {
            Data = data;
        }

        public T Data { get; }
    }
}
