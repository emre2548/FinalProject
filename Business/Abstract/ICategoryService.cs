using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        // Category ile ilgili dış dünyaya neyi servis etmek istiyorsak o fonksiyonları yazıyoruz

        IDataResult<List<Category>> GetAll();
        IDataResult<Category> GetById(int categoryId);

    }
}
