using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants
{
    // static verince new'lemeye gerek yok
    public static class Messages
    {
        // public PASCAL CASE yazılır ilk harf büyük olur değişkende
        public static string ProductAdded = "Ürün Eklendi";
        public static string ProductNameInvalid = "Ürün ismi geçersiz";
        public static string MaintenanceTime = "Sistem bakımda";
        public static string ProductsListed = "Ürünler listelendi";

        public static string ProductCauntOfCategoryError = "Bir kategoride en fazla 15 ürün olabilir";

        public static string ProductUpdated = "Ürün güncellendi";
        public static string ProductNameAlreadyExists = "Bu isimde başka bir ürün var";

        public static string CategoryLimitExceded = "Kategori limiti aşıldığı için yeni ürün eklenemiyor";
    }
}
