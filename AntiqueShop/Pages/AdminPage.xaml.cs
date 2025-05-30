using Microsoft.Win32;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        AntiqueShopEntities3 db = new AntiqueShopEntities3();
        List<Items> items;

        public AdminPage()
        {
            InitializeComponent();
            LoadData();
            this.Loaded += AdminPage_Loaded;
        }

        private void LoadData()
        {
            items = db.Items.ToList();

            var categories = db.Categories.ToList();
            categories.Insert(0, new Categories { id_category = 0, name_category = "Все" });

            ComboFilter.ItemsSource = categories;
            ComboFilter.DisplayMemberPath = "name_category";
            ComboFilter.SelectedIndex = 0; // "Все" будет выбран по умолчанию

            ApplyFilters();
        }
        private void AdminPage_Loaded(object sender, RoutedEventArgs e)
        {
            db = new AntiqueShopEntities3(); // Пересоздаём контекст
            LoadData(); // Загружаем данные и обновляем список
        }


        private void ApplyFilters()
        {
            var filtered = items.AsEnumerable();

            // Фильтр по категории
            if (ComboFilter.SelectedItem is Categories category && category.id_category != 0)
            {
                filtered = filtered.Where(i => i.id_category == category.id_category);
            }

            // Поиск по названию
            if (!string.IsNullOrWhiteSpace(TextSearch.Text))
                filtered = filtered.Where(i => i.name_item.ToLower().Contains(TextSearch.Text.ToLower()));

            // Сортировка
            switch ((ComboSort.SelectedItem as ComboBoxItem)?.Content?.ToString())
            {
                case "По году (возрастание)":
                    filtered = filtered.OrderBy(i => i.year);
                    break;
                case "По году (убывание)":
                    filtered = filtered.OrderByDescending(i => i.year);
                    break;
                case "По цене покупки (возрастание)":
                    filtered = filtered.OrderBy(i => i.purchase_price);
                    break;
                case "По цене покупки (убывание)":
                    filtered = filtered.OrderByDescending(i => i.purchase_price);
                    break;
                case "По дате поступления (новые)":
                    filtered = filtered.OrderByDescending(i => i.arrival_date);
                    break;
                case "По дате поступления (старые)":
                    filtered = filtered.OrderBy(i => i.arrival_date);
                    break;
            }

            listItems.ItemsSource = filtered.ToList();
            tbCounter.Text = $"Найдено товаров: {listItems.Items.Count}";
        }

        private void TextSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void ComboFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void ComboSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditPage(null));
        }

        private void EditItem_Click(object sender, RoutedEventArgs e)
        {
            if (listItems.SelectedItem is Items selectedItem)
            {
                NavigationService.Navigate(new AddEditPage(selectedItem));
            }
            else
            {
                MessageBox.Show("Выберите товар для редактирования.");
            }
        }

        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {
            if (listItems.SelectedItem is Items selected)
            {
                if (MessageBox.Show("Удалить этот товар?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    db.Items.Remove(selected);
                    db.SaveChanges();
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Выберите товар для удаления.");
            }
        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            var currentItems = listItems.ItemsSource.Cast<Items>().ToList();
            ExportToExcel(currentItems);
        }

        private void ExportToExcel(List<Items> exportItems)
        {
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "CSV файлы (*.csv)|*.csv|Excel файлы (*.xlsx)|*.xlsx",
                    FileName = "Список_товаров.csv"
                };

                if (saveDialog.ShowDialog() == true)
                {
                    var sb = new StringBuilder();

                    // Заголовки
                    sb.AppendLine("Название;Год;Состояние;Подлинность;Цена покупки;Цена продажи;Дата поступления");

                    // Данные
                    foreach (var item in exportItems)
                    {
                        sb.AppendLine($"\"{item.name_item}\";{item.year};{item.condition};{item.authenticity};{item.purchase_price};{item.selling_price};{item.arrival_date?.ToShortDateString()}");
                    }

                    File.WriteAllText(saveDialog.FileName, sb.ToString(), Encoding.UTF8);
                    MessageBox.Show("Файл успешно сохранён!", "Экспорт", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при экспорте: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OpenLogs_Click(object sender, RoutedEventArgs e)
        {
            if (listItems.SelectedItem is Items selected)
            {
                NavigationService.Navigate(new ItemLogsPage(selected));
            }
            else
            {
                MessageBox.Show("Выберите товар для просмотра журнала изменений.");
            }
        }

        private void UserInfo_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UserInfoPage());
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LoginPage());
        }

        private void listItems_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (listItems.SelectedItem is Items selected)
            {
                NavigationService.Navigate(new ItemDetailsPage(selected));
            }
        }
        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            db = new AntiqueShopEntities3(); // Обновим контекст, если были изменения
            LoadData();
        }
    }
}
