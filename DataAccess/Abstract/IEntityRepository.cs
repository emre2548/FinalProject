using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IEntityRepository<T>
    {
        /* buradaki yapıyı ICategoryDal ve IProductDal içinde kullanıyoruz aynısını sadece tipi değişiyor 
         * her iki interface de ayrı ayrı yazmaya gerek kalmadan buradan T ile gelen tipi alıp o tipte işlem 
         * yapabiliriz enttiy burada genel isim */

        /* yapılan sorguda GetAll(Expression<Func<T,bool>> filter=null) filtreleme yapabilmemizi sağlar 
         * filter=null filtrede vermeyebilirsin demek */
        List<T> GetAll(Expression<Func<T,bool>> filter=null);
        // tek bir data getirmek için bir kişi ait bilgiler gibi gibi
        T Get(Expression<Func<T, bool>> filter);
        void Add(T enttiy);
        void Update(T enttiy);
        void Delete(T enttiy);

        /* yukarıdaki GettAll filtresiden dolayı bu koda ihtiyaç kalmadı */
        //List<T> GetAllByCategory(int categoryId);
    }
}
