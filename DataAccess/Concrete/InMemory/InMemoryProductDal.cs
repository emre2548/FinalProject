using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryProductDal : IProductDal
    {

        // bu clas için global değişken olduğu için _ ile isimlendirilir. (Naming Comantial)
        List<Product> _products;

        // ctor tabtab ile constractır oluştur
        public InMemoryProductDal()
        {
            // burası bir veri tabanından geliyormuş gibi bize simule edecek
            _products = new List<Product> {
                new Product{ProductId=1,CategoryId=1,ProductName="Bardak",UnitPrice=15,UnitsInStock=15},
                new Product{ProductId=2,CategoryId=1,ProductName="Kamera",UnitPrice=500,UnitsInStock=3},
                new Product{ProductId=3,CategoryId=2,ProductName="Telefon",UnitPrice=1500,UnitsInStock=2},
                new Product{ProductId=4,CategoryId=2,ProductName="Klavye",UnitPrice=150,UnitsInStock=65},
                new Product{ProductId=5,CategoryId=2,ProductName="Fare",UnitPrice=85,UnitsInStock=1},
            };
        }

        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Delete(Product product)
        {
            /* burada silme işlemi yaparken _products.Remove(product); yazarsak listedekiler bellekte heap alanında ve referans adresleri
             * ile tutuluyor sil dediğmizde id vs aynı olsa bile referans adresi farklı olduğundan silmez bunun için aşağıdaki yöntem ile siliyoruz*/

            // burada id'den yukarıdaki nesnenin referans adresine ulaşacağız böylce silebileceğiz

            //Product productToDelte = null;
            //foreach (var p in _products)
            //{
            //    if (product.ProductId == p.ProductId)
            //    {
            //        productToDelte = p; // bir product oluşturduk ve referansı attık
            //    }
            //}

            //_products.Remove(productToDelte);


            // LINQ - Language Integrated Quert  metodu
            // foreach döngüsünün LINQ ile yazıyoruz
            /* p=> --> tek tek dolaşırken kullanacağı takma isim ID bazlı yapılarda SingleOrDefault kullanabiliriz ikitane sonuç gelirse hata verir bu yüzden ID 
             * bazlı yapılarda kullanıyoruz*/
            Product productToDelte = _products.SingleOrDefault(p => p.ProductId == product.ProductId);

            _products.Remove(productToDelte);

        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll()
        {
            // veritabanındanki datayı business'a veriyoruz
            return _products;
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAllByCategory(int categoryId)
        {
            return _products.Where(p => p.CategoryId == categoryId).ToList();
        }

        public void Update(Product product)
        {
            // Gönderilen ürün ID'sine sahip ürünü bul
            Product productToUpdate = _products.SingleOrDefault(p => p.ProductId == product.ProductId);

            productToUpdate.ProductName = product.ProductName;
            productToUpdate.CategoryId = product.CategoryId;
            productToUpdate.UnitPrice = product.UnitPrice;
            productToUpdate.UnitsInStock = product.UnitsInStock;
        }
    }
}
