using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")] // istek yaparken nasıl ulaşılacağını belirtiyor
    [ApiController] // Attibute
    public class ProductsController : ControllerBase
    {
        /* hiç bir katman somut üzerinden gitmiyor soyut üzerinden gidiyoruz
         * IProductServer ProductManager referansını tutyor*/
        // naming convention
        // Loosly coupled (Gevşek bağlılık)
        // IoC Container --> Inversion of Control (Değişimin kontrolü)
        IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("getall")]
        //public List<Product> Get() // Startup altına referansları belirtince değiştirdik
        public IActionResult GetAll()
        {
            //return new List<Product>
            //{
            //    new Product { ProductId = 1, ProductName = "Elma" },
            //    new Product { ProductId = 2, ProductName = "Armut" },
            //};

            // ProductManager'a bağlı olmuş olduk (Dependency Chain)
            //IProductService productService = new ProductManager(new EfProductDal());
            //var result = productService.GetAll();
            //var result = _productService.GetAll();
            //return result.Data;

            // Swagger --> api'den bilgi çekecekler için dökümantasyon sağlar api'den bilgi alan ona göre arayüzü yapılandırır
            var result = _productService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _productService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /* post request ben sana data vericem bunun sistemine ekle demek */
        [HttpPost("add")]
        public IActionResult Add(Product product)
        {
            var result = _productService.Add(product);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


    }
}
