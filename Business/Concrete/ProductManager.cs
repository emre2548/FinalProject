using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.CCS;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        /* bir iş sınıfı başka iş sınıflarını new'lemez */


        /* ProductManager new'lendiği zaman bize birtane IProduct referansı ver dedik */

        // burada enttiy framework'e bağlı olmadık eklemeler yaptık ama bozulmadı
        IProductDal _productDal;
        //ILogger _logger;
        // dependice injection oluyor
        /* bir Entity Manage kendisi hariç başka dalı enjekte edemez */
        /* başka bir servisi enjete edebiliriz ileride kural değişirse her yerden değiştirmek durumunda kalmayız */
        ICategoryService __categoryService;
        public ProductManager(IProductDal productDal/*, ILogger logger*/,ICategoryService categoryService)
        {
            _productDal = productDal;
            //_logger = logger;
            __categoryService = categoryService; // sonradna eklenen kural için kategori ürünler 15'i geçemez
        }

        [SecuredOperation("product.add")]
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

            // Logger için
            //_logger.Log();
            //try
            //{
            //    _productDal.Add(product);
            //    return new SuccessResult(Messages.ProductAdded);
            //}
            //catch (Exception exception)
            //{
            //    _logger.Log();
            //}
            //return new ErrorResult();
            // validation kodu aspect yapacak

            // bir kategoride en fazla 10 ürün olabilir
            //var result = _productDal.GetAll(p =>p.CategoryId == product.CategoryId).Count;
            //if (result >= 10)
            //{
            //    return new ErrorResult(Messages.ProductCauntOfCategoryError);
            //}
            // alt satır bizim için iş kurallarını çalıştıracak
            // yeni kural --> eğer mevcut kategori sayısı 15 geçityse sisteme yeni ürün eklenemez
            IResult result = BusinessRules.Run(CheckIfProductNameExists(product.ProductName), 
                CheckIfProductCountOfCategory(product.CategoryId),
                CheckIfCategoryLimitExceded());
            if (result != null)
            {// kurala uymayan durum var ise
                return result;
            }
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
            //if (CheckIfProductCountOfCategory(product.CategoryId).Success)
            //{
            //    if (CheckIfProductNameExists(product.ProductName).Success)
            //    {
            //        _productDal.Add(product);
            //        return new SuccessResult(Messages.ProductAdded);

            //    }
            //}
            //return new ErrorResult();

            // Aynı isimde ürün eklenemez

            //_productDal.Add(product);

            // returnde 2 parametre döndürebilmek için contractor'a parametre eklemek gerek
            //return new SuccessResult(Messages.ProductAdded); // IResult yapınca bizden return bekliyor (true,"Ürün eklendi.") true kaldırıldı
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

            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductsListed);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            // kategory id benim gönderdiğim kategory id eşit ise onları filtrele
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id)); // burada çalıştığın tipi belli ediyorsun
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
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

        [ValidationAspect(typeof(ProductValidator))]
        public IResult Update(Product product)
        {
            if (CheckIfProductCountOfCategory(product.CategoryId).Success)
            {
                _productDal.Update(product);
                return new SuccessResult(Messages.ProductAdded);
            }
            return new ErrorResult();
        }

        // bu bir iş kuralı parçacığı bir servis değil bundan dolayı buraya ve private olarak yazıyourz
        private IResult CheckIfProductCountOfCategory(int categoryId)
        {
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;
            if (result >= 15)
            {
                return new ErrorResult(Messages.ProductCauntOfCategoryError);
            }
            return new SuccessResult();
        }

        private IResult CheckIfProductNameExists(string productName)
        {
            // Any() şuna uyan kayıt var mı bool döndürür
            var result = _productDal.GetAll(p => p.ProductName == productName).Any();
            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }
            return new SuccessResult();
        }

        private IResult CheckIfCategoryLimitExceded()
        {
            var result = __categoryService.GetAll();
            if (result.Data.Count>15)
            {
                return new ErrorResult(Messages.CategoryLimitExceded);
            }
            return new SuccessResult();
        }
    }
}
