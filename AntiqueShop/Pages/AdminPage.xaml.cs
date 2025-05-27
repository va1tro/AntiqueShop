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
        }

        private void LoadData()
        {
            items = db.Items.ToList();
            ComboFilter.ItemsSource = db.Categories.ToList();
            ComboFilter.DisplayMemberPath = "name_category";
            ComboFilter.SelectedIndex = -1;
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            var filtered = items.AsEnumerable();

            // Фильтр по категории
            if (ComboFilter.SelectedItem is Categories category)
                filtered = filtered.Where(i => i.id_category == category.id_category);

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
            if (listItems.SelectedItem is Items selected)
            {
                NavigationService.Navigate(new AddEditPage(selected));
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
                    Filter = "Excel файлы (*.xlsx)|*.xlsx",
                    FileName = "Список_товаров.xlsx"
                };

                if (saveDialog.ShowDialog() == true)
                {
                    using (ExcelPackage package = new ExcelPackage())
                    {
                        ExcelWorksheet sheet = package.Workbook.Worksheets.Add("Товары");

                        // Заголовки
                        sheet.Cells[1, 1].Value = "Название";
                        sheet.Cells[1, 2].Value = "Год";
                        sheet.Cells[1, 3].Value = "Состояние";
                        sheet.Cells[1, 4].Value = "Подлинность";
                        sheet.Cells[1, 5].Value = "Цена покупки";
                        sheet.Cells[1, 6].Value = "Цена продажи";
                        sheet.Cells[1, 7].Value = "Дата поступления";

                        // Стиль заголовков
                        using (var range = sheet.Cells[1, 1, 1, 7])
                        {
                            range.Style.Font.Bold = true;
                            range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Beige);
                            range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        }

                        int row = 2;
                        foreach (var item in exportItems)
                        {
                            sheet.Cells[row, 1].Value = item.name_item;
                            sheet.Cells[row, 2].Value = item.year;
                            sheet.Cells[row, 3].Value = item.condition;
                            sheet.Cells[row, 4].Value = item.authenticity;
                            sheet.Cells[row, 5].Value = item.purchase_price;
                            sheet.Cells[row, 6].Value = item.selling_price;
                            sheet.Cells[row, 7].Value = item.arrival_date?.ToShortDateString();
                            row++;
                        }

                        // Автоширина
                        sheet.Cells[sheet.Dimension.Address].AutoFitColumns();

                        // Сохраняем
                        File.WriteAllBytes(saveDialog.FileName, package.GetAsByteArray());
                        MessageBox.Show("Файл успешно сохранён!", "Экспорт", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
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
    }
}
