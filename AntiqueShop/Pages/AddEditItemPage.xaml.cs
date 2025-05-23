using AntiqueShop.AppData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AntiqueShop.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditItemPage.xaml
    /// </summary>
    public partial class AddEditItemPage : Page
    {
        private AntiqueShopEntities3 db = new AntiqueShopEntities3();
        public AddEditItemPage()
        {
            InitializeComponent();
            fff.ItemsSource = db.Role.ToList();
        }
    }
}
