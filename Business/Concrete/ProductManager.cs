using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        /* bir iş sınıfı başka iş sınıflarını new'lemez */


        /* ProductManager new'lendiği zaman bize birtane IProduct referansı ver dedik */
        IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public List<Product> GetAll()
        {
            //İş kodları yazılır
            return _productDal.GetAll();
        }
    }
}
