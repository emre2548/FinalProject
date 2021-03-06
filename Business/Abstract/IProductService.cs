﻿using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IProductService
    {
        // dataAccess ve Entity'yi project referans olarak Add altından ekledik
        //List<Product> GetAll();
        IDataResult<List<Product>> GetAll(); //  işlem sonucunu ve mesajıda döndüreceğiz dedik

        IDataResult<List<Product>> GetAllByCategoryId(int id);

        IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max);

        IDataResult<List<ProductDetailDto>> GetProductDetails();

        IDataResult<Product> GetById(int productId);

        IResult Add(Product product); // void olan yerleri IResult yapıyoruz

        /* RSTFUL --> HTTP -->  */

        IResult Update(Product product);

        IResult AddTransactionalTest(Product product);
        //IDataResult<List<Product>> GetByCategoryId(int categoryId);
    }
}
