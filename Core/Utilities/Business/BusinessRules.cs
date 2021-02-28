using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Business
{
    public class BusinessRules
    {
        /* params verince istediğimiz kadar IResult verebiliriz parametre olarak bir diziye atmış olduk
         * logics --> iş kuralı demek virgül ile istediğimiz kadar parametre geçebiliriz*/
        public static IResult Run(params IResult[] logics)
        {
            foreach (var logic in logics)
            {
                if (!logic.Success)
                {
                    return logic; // parametre ile gönderilen iş kurallarından başarısız olanları gönderiyoruz
                }
            }
            return null;

            // liste olarak 
            // result dediğimiz kurala uymayanlar
            //List<IResult> errorResult = new List<IResult>();
            //foreach (var logic in logics)
            //{
            //    if (!logic.Success)
            //    {
            //        errorResult.Add(logic); // parametre ile gönderilen iş kurallarından başarısız olanları gönderiyoruz
            //    }
            //}
            //return errorResult; // diğer tarafta da boş mu değil mi kntrol ederisn
        }
    }
}
