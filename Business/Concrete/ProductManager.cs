using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
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

        /* add methodunun productvalidator methodundaki kurallara göre doğrula demek */
        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {
            // Business codes
            // Validation codes
            /* bussines code ayrı validation code ayrı olmalı  */
            /* iş kuralı bizim iş ihtiyacımıza uygunluğuı belirtir birine ehliyet verme uygunluğu kuralları gibi sağlığı filan gibi 
             * ya da kredi alacak kişinin finansal duruma bakmak gibi şeyler iş kodu oluyor
              eklenmesini isteidğimiz nesnenin özellikleri ise validation kodu oluyor */

            /* kuralları buraya yazmak yerine merjezi bir yere yazacağız buradaki kodları DependecyResolves.FluentValidation.ProductValidator içine yazdık */
            //if (product.UnitPrice <= 0)
            //{
            //    return new ErrorResult(Messages.UnitPriceInvalid);
            //}

            //if (product.ProductName.Length < 2)
            //{
            //    // magic string
            //    return new ErrorResult(Messages.ProductNameInvalid);
            //}

            /* bir validation yapılacağı zaman start code */
            //var context = new ValidationContext<Product>(product);
            //ProductValidator productValidator = new ProductValidator();
            //var result = productValidator.Validate(context);
            //if (!result.IsValid)
            //{
            //    throw new ValidationException(result.Errors);
            //}
            /* yukarıdaki kodu tekrar yazmamak için bizim yazdıımız tools olsun */
            //ValidationTool.Validate(new ProductValidator(), product); // [ValidationAspect(typeof(ProductValidator))] satırı ekleyince sildik

            _productDal.Add(product);

            // returnde 2 parametre döndürebilmek için contractor'a parametre eklemek gerek
            return new SuccessResult(Messages.ProductAdded); // IResult yapınca bizden return bekliyor (true,"Ürün eklendi.") true kaldırıldı
        }

        public IDataResult<List<Product>> GetAll()
        {
            //İş kodları yazılır
            // Yetkisi var mı?

            //if (DateTime.Now.Hour == 23)
            //{
            //    //MaintenanceTime bakım zamanı demek default ürün listesi null oluyor
            //    return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            //}

            return new SuccessDataResult<List<Product>>(_productDal.GetAll(),Messages.ProductsListed);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            // kategory id benim gönderdiğim kategory id eşit ise onları filtrele
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p=>p.CategoryId==id)); // burada çalıştığın tipi belli ediyorsun
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p=>p.ProductId == productId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            //if (DateTime.Now.Hour == 21)
            //{
            // sistem bakımda mesajı gönderiyoruz
            //    //MaintenanceTime bakım zamanı demek default ürün listesi null oluyor
            //    return new ErrorDataResult<List<ProductDetailDto>>(Messages.MaintenanceTime);
            //}
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }
    }
}
