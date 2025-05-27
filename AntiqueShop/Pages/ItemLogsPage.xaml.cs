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
    /// Логика взаимодействия для ItemLogsPage.xaml
    /// </summary>
    public partial class ItemLogsPage : Page
    {
        public ItemLogsPage(Items selectedItem)
        {
            InitializeComponent();
            LoadLogs(selectedItem.id_item);
        }

        private void LoadLogs(int itemId)
        {
            using (var context = new AntiqueShopEntities3())
            {
                var logs = context.Item_logs
                                  .Where(l => l.id_item == itemId)
                                  .OrderByDescending(l => l.change_date)
                                  .ToList();
                LogsDataGrid.ItemsSource = logs;
            }
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
