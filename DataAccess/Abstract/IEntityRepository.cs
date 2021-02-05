using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Abstract
{
    // generic constrain -- generik kısıt demek buraya gelecek değişken tiplerini sınırlandırıyoruz
    // class : referans tip olabilir demek
    // IEnttiy : I enttiy olabilir veya IEntity implemente edebilen bir nesne olabilir
    // new() ; new'lenebilir olmalı
    // T -- IEntity olabilir ya da IEntity'den implemente edilen bir nesne olabilir demek
    // IEntity'yi new ile devre dışı bıraktık çünkü IEntity new'lenemez  new ile newlenebilir olmalı dedik böylece IEntity gelemez

    /* Ayakları üzerinde durabilen bir backend yazmış olduk */
    public interface IEntityRepository<T> where T:class,IEntity,new()
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
