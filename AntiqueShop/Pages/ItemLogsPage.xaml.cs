using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
    public partial class ItemLogsPage : Page
    {
        private int itemId;

        public ItemLogsPage(Items selectedItem)
        {
            InitializeComponent();
            itemId = selectedItem.id_item;
            LoadLogs();
        }

        private void LoadLogs()
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

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            string fieldFilter = FieldFilterBox.Text?.ToLower();
            DateTime? startDate = StartDatePicker.SelectedDate;
            DateTime? endDate = EndDatePicker.SelectedDate?.AddDays(1); // включительно

            using (var context = new AntiqueShopEntities3())
            {
                var filteredLogs = context.Item_logs
                                          .Where(l => l.id_item == itemId)
                                          .AsQueryable();

                if (!string.IsNullOrWhiteSpace(fieldFilter))
                    filteredLogs = filteredLogs.Where(l => l.changed_field.ToLower().Contains(fieldFilter));

                if (startDate.HasValue)
                    filteredLogs = filteredLogs.Where(l => l.change_date >= startDate.Value);

                if (endDate.HasValue)
                    filteredLogs = filteredLogs.Where(l => l.change_date < endDate.Value);

                LogsDataGrid.ItemsSource = filteredLogs
                    .OrderByDescending(l => l.change_date)
                    .ToList();
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
