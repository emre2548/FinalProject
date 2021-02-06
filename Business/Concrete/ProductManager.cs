using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        /* bir iş sınıfı başka iş sınıflarını new'lemez */


        /* ProductManager new'lendiği zaman bize birtane IProduct referansı ver dedik */

        // burada enttiy framework'e bağlı olmadık eklemeler yaptık ama bozulmadı
        IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public List<Product> GetAll()
        {
            //İş kodları yazılır
            // Yetkisi var mı?
            return _productDal.GetAll();
        }

        public List<Product> GetAllByCategoryId(int id)
        {
            // kategory id benim gönderdiğim kategory id eşit ise onları filtrele
            return _productDal.GetAll(p=>p.CategoryId==id);
        }

        public List<Product> GetByUnitPrice(decimal min, decimal max)
        {
            return _productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max);
        }

        public List<ProductDetailDto> GetProductDetails()
        {
            return _productDal.GetProductDetails();
        }
    }
}
