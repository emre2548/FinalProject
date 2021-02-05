using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using System;

namespace ConsoleUI
{

    // SOLID 
    // Yaptığımız yazılıma yeni bir özelilk ekliyorsak mevcuttaki hiçbir koduna dokunamazsın buda O harfi oluyor Open Closed Principle oluyor

    class Program
    {
        static void Main(string[] args)
        {
            ProductManager productManager = new ProductManager(new EfProductDal());

            foreach (var product in productManager.GetByUnitPrice(40,100))
            {
                Console.WriteLine(product.ProductName);
            }

        }
    }
}
