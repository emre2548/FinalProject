using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        // kurallar contructor (ctor ) içerisine yazılır
        public ProductValidator()
        {
            /* product Manager içerisinde yazdığımın 
             *  if (product.ProductName.Length < 2)
            {
                // magic string
                return new ErrorResult(Messages.ProductNameInvalid);
            }
            kurala karşılık geliyor */
            RuleFor(p => p.ProductName).NotEmpty();
            RuleFor(p => p.ProductName).MinimumLength(2);
            RuleFor(p => p.UnitPrice).NotEmpty();
            RuleFor(p => p.UnitPrice).GreaterThan(0);
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(10).When(p => p.CategoryId == 1);


            /* örneğin ürünlerin ismi A ile başlamalı gib ya da olmayan bir kuralı yazma */
            // false dönerse bu satır patlar
            // çoklu dil desteği var ama .withmessage ile mesaj yazılabilir
            RuleFor(p => p.ProductName).Must(StartWithA).WithMessage("Ürünler A harfi ile başlamalıdır"); 
        }

        private bool StartWithA(string arg)
        {
            return arg.StartsWith("A");
        }
    }
}
