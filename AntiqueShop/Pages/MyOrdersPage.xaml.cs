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
    /// Логика взаимодействия для MyOrdersPage.xaml
    /// </summary>
    public partial class MyOrdersPage : Page
    {
        public MyOrdersPage()
        {
            InitializeComponent();
            LoadOrders();
        }

        private void LoadOrders()
        {
            try
            {
                var currentUser = AppConnect.CurrentUser;

                var orders = AppConnect.model0db.Sales
                    .Where(s => s.id_user == currentUser.id_user)
                    .OrderByDescending(s => s.sale_date)
                    .ToList();

                ordersList.ItemsSource = orders;

                tbCounter.Text = orders.Any()
                    ? $"Оформленных заказов: {orders.Count}"
                    : "У вас нет оформленных заказов.";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки заказов: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
