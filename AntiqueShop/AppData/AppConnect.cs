using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntiqueShop.AppData
{
    internal class AppConnect
    {
        public static AntiqueShopEntities3 model0db; // Замените YourDbContext на ваш класс контекста

        static AppConnect()
        {
            model0db = new AntiqueShopEntities3(); // Инициализация при первом обращении
        }
    }
}
