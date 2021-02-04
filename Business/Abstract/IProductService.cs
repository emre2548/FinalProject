using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IProductService
    {
        // dataAccess ve Entity'yi project referans olarak Add altından ekledik
        List<Product> GetAll(); 
    }
}
