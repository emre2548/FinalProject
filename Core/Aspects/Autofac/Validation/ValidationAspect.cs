using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType)
        {
            // attribute'de type ile geçmek zorundasın
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new System.Exception("Bu bir doğrulama sınıfı değil");
            }

            _validatorType = validatorType;
        }

        // validation için onbefore boş burada ovverride ettik ve içini doldurduk bu kod çalışacak
        protected override void OnBefore(IInvocation invocation)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType); // reflection oluyor --> çalışma anında birşeyleri çalıştırmaya yarar
            // çalışma anında new ler
            var entityType = _validatorType.BaseType.GetGenericArguments()[0]; // product validator çalışma tipini bul (generic çalıştığı ilkini bul)
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType); // bulduğun tipin parametrelerini bul invocation method demek
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}
