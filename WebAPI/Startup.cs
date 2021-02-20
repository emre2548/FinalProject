using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            /* bize arka planda bir referans olu�tur bizim IoC bizim yerimize arka planda new'ler
             * i�erisinde data tutmuyorsan singeleton kullan gelen b�t�n isteklere ayn� referans� verir 
             * e�er bir sepete �r�n ekleme durumu varsa onda kullan�rsan biri sepe �r�n ekleyince herkeze gider */
            // Mimainin Ad� --> Autofac, Minject, CastleWindsor, StructureMap, Lightject, DryInject --> IoC Cantainer
            // AOP --> b�t�n mesajlar� loglamak istedi�imiz d���nelim bunun IloggerService �a��r�r�z bunda [LogAspect] yazarak loglama yapar�z yani Business i�inde business yaz�l�r
            // Autofac bize AOP ikan� sunuyor bu y�zden .NET IOC container'a autofac enjekte edece�iz
            // AutoFac ile ilerleyece�iz
            // bu ayarlar�n yeri do�ru bir yap� i�in buras� olamamal� autofac kullanaca��z daha arka plana ataca��z
            //services.AddSingleton<IProductService, ProductManager>();
            //services.AddSingleton<IProductDal, EfProductDal>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}