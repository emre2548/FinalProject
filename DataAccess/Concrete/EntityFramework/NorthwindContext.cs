using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    // Context : Db tablosu ile proje classlarını bağlar
    // DbContext --> entity framework NyGet pack ile geliyor nuget pack kısmından EntityFrameworkCore.SqlServer paketini kurduk
    public class NorthwindContext:DbContext
    {
        /* Hangi db ile ilişkilendirdiğimizi belirttiğimiz yer */
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // sql server'a nasıl bağlanacağını belirtiyoruz
            // (localdb)\mssqllocaldb büyük küçük harf kuralına bağlı değildir SQL server'da db'e göre değişir
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=Northwind;Trusted_Connection=true");
        }


        // Hangi class hangi tabloya karşılık geliyor

        // DbSet<NESNE> TABLO_ADI
        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Order> Orders { get; set; }


    }
}
