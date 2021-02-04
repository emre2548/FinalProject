using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    // interfacenin  kendisi public değil ama operasyonları public
    public interface IProductDal:IEntityRepository<Product>
    {
        /* IEntityRepository<Product> product için yapılandır dedik ve aşağıdaki fonksiyonları oradan aldık */
        //List<Product> GetAll();

        //void Add(Product product);
        //void Update(Product product);
        //void Delete(Product product);

        //List<Product> GetAllByCategory(int categoryId);
    }
}
