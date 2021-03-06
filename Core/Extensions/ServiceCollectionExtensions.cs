using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Extensions
{
    /* extansions yazabilmemiz için o class'ın static olması gerekiyor */
    public static class ServiceCollectionExtensions
    {
        /* IServiceCollection API'mizin servis bağımlılıkları ya da araya girmesini istediğimiz servislerin yazıldığı yer */
        /* this IServiceCollection serviceCollection parametre değil neyi genişletmek istiyorsak onu yazıyoru C#'da bu şekilde */
        public static IServiceCollection AddDependencyResolvers(this IServiceCollection serviceCollection, ICoreModule[] modules)
        {
            foreach (var module in modules)
            {
                module.Load(serviceCollection);
            }
            return ServiceTool.Create(serviceCollection);
        }
    }
}
