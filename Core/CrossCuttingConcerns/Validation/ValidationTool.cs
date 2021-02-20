using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Validation
{
    public class ValidationTool
    {
        /* bu tip araçlar static yapılır bellek oluşturulur ve o kullanılır tekrar tekar new lememek içn */

        /* kodu refactor yapıp evrensel kullanılabilir hale getiriyoruz */
        public static void Validate(IValidator validator, object entity)
        {
            var context = new ValidationContext<object>(entity);
            //ProductValidator productValidator = new ProductValidator(); // IValidator validator ekleyince bu satıra gerek kalmadı
            var result = validator.Validate(context);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}
