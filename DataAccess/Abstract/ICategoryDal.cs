﻿using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface ICategoryDal:IEntityRepository<Category>
    {
        // Generic Repository Disagn Petern yapının adı 
        //List<Category> GetAll();
        //void Add(Category category);
        //void Update(Category category);
        //void Delete(Category category);
        //List<Category> GetAllByCategory(int categoryId);
    }
}
