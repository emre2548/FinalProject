using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Abstract;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
    // NuGet paketini kullanacağız
    public class EfProductDal : EfEntityRepositoryBase<Product, NorthwindContext>, IProductDal
    {


        /* EfEntityRepositoryBase içerisinde IProductDal işlemleri var core içine taşıdıktan sonra core kısmına
         * oluşturuduğumuz generic yapıyı buraya referans alıyoruz  öylece IProductDal hata vermiyor 
         * şuan veritabanı operasyonları yazmaya hazırız */








        /* ----------------------------- Core solition altında EfEntityRepostoryBase clasın'a taşıdık -----------------------------  */
        //public void Add(Product enttiy)
        //{
        //    // usi tab tab C# özel using içerisine yazılan nesneler işi bitince garbage collector bellekten temizler
        //    // IDisposable pattern implemantation of C#
        //    using (NorthwindContext context = new NorthwindContext())
        //    {
        //        // referans numarasının tutacağız temizleme için 
        //        // Git verikayanğından benim gönderdiğim producttan biri ile eşleştir ama ekleme oldupğu için eşleştırme olmayacak bunu
        //        // addedEntity ile aşlaştiriyoruz
        //        var addedEntity = context.Entry(enttiy);
        //        // enttiy framework'de added ile referansı yakıyoruz
        //        addedEntity.State = EntityState.Added;
        //        context.SaveChanges(); // ekledik
        //    }
        //}

        //public void Delete(Product enttiy)
        //{
        //    using (NorthwindContext context = new NorthwindContext())
        //    {
        //        var deletedEntity = context.Entry(enttiy);
        //        deletedEntity.State = EntityState.Deleted;
        //        context.SaveChanges();
        //    }
        //}

        //public Product Get(Expression<Func<Product, bool>> filter)
        //{
        //    // Tek data getirecek
        //    using (NorthwindContext context = new NorthwindContext())
        //    {
        //        // product tablosu bu filtreyi oraya uygula dedik
        //        return context.Set<Product>().SingleOrDefault(filter);
        //    }
        //}

        //public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        //{
        //    using (NorthwindContext context = new NorthwindContext())
        //    {
        //        // filtre verdiysek filtreyi geçtimi geçmedimi
        //        // filtre null ise ilk kısım çalışır değil ise ikinci kısım çalışır
        //        // veritabanındaki bütün tabloyu listeye çevir ve onu bize ver
        //        // arka planda select * from product çalıştırıyor
        //        // filtre null ise  context.Set<Product>().ToList()  çalışacak 
        //        // filtre null değilse  context.Set<Product>().Where(filter).ToList(); çalışacak
        //        return filter == null ? context.Set<Product>().ToList() : context.Set<Product>().Where(filter).ToList();
        //    }
        //}

        //public void Update(Product enttiy)
        //{
        //    using (NorthwindContext context = new NorthwindContext())
        //    {
        //        var updatedEntity = context.Entry(enttiy);
        //        updatedEntity.State = EntityState.Modified;
        //        context.SaveChanges();
        //    }
        //}
        public List<ProductDetailDto> GetProductDetails()
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                // ürünler ile kategorileri join et on dan sonraki kısma göre yap sonucu
                // select den sonrkai kısma göre ver
                var result = from p in context.Products
                             join c in context.Categories
                             on p.CategoryId equals c.CategoryId
                             select new ProductDetailDto 
                             { 
                                 ProductId = p.ProductId, ProductName = p.ProductName, 
                                 CategoryName = c.CategoryName, UnitsInStock = p.UnitsInStock 
                             };

                return result.ToList();
            }
        }
    }
}
