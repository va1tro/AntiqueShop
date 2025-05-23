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
    /// Логика взаимодействия для ProductsPage.xaml
    /// </summary>
    public partial class ProductsPage : Page
    {
        public ProductsPage()
        {
            InitializeComponent();
            LoadItems();
            LoadFiltersAndSorting();
        }

        private void LoadItems()
        {
            var items = AppConnect.model0db.Items.ToList();
            listItems.ItemsSource = items;
            UpdateCounter(items.Count);
        }

        private void LoadFiltersAndSorting()
        {
            // Загрузка категорий для фильтра
            var categories = new List<string> { "Все" };
            categories.AddRange(AppConnect.model0db.Categories.Select(c => c.name_category));
            ComboFilter.ItemsSource = categories;
            ComboFilter.SelectedIndex = 0;

            // Установка сортировки по умолчанию
            ComboSort.SelectedIndex = 0;
        }

        private void UpdateCounter(int count)
        {
            tbCounter.Text = count > 0 ? $"Найдено товаров: {count}" : "Товары не найдены";
        }

        private List<Items> FindItems()
        {
            var items = AppConnect.model0db.Items.ToList();

            // Фильтрация по поиску
            if (!string.IsNullOrWhiteSpace(TextSearch.Text))
            {
                items = items.Where(i => i.name_item.ToLower()
                                             .Contains(TextSearch.Text.ToLower()))
                                             .ToList();
            }

            // Фильтрация по категории
            if (ComboFilter.SelectedIndex > 0)
            {
                items = items.Where(i => i.Categories.name_category ==
                                        ComboFilter.SelectedItem.ToString())
                                        .ToList();
            }

            // Сортировка
            switch (ComboSort.SelectedIndex)
            {
                case 1: // По году (возрастание)
                    items = items.OrderBy(i => i.year).ToList();
                    break;
                case 2: // По году (убывание)
                    items = items.OrderByDescending(i => i.year).ToList();
                    break;
                case 3: // По цене покупки (возрастание)
                    items = items.OrderBy(i => i.purchase_price).ToList();
                    break;
                case 4: // По цене покупки (убывание)
                    items = items.OrderByDescending(i => i.purchase_price).ToList();
                    break;
                case 5: // По дате поступления (новые)
                    items = items.OrderByDescending(i => i.arrival_date).ToList();
                    break;
                case 6: // По дате поступления (старые)
                    items = items.OrderBy(i => i.arrival_date).ToList();
                    break;
            }

            UpdateCounter(items.Count);
            return items;
        }

        //private void BtnAdd_Click(object sender, RoutedEventArgs e)
        //{
        //    NavigationService.Navigate(new AddEditItemPage(new Items()));
        //}

        //private void BtnEdit_Click(object sender, RoutedEventArgs e)
        //{
        //    if (listItems.SelectedItem is Items selectedItem)
        //    {
        //        NavigationService.Navigate(new AddEditItemPage(selectedItem));
        //    }
        //    else
        //    {
        //        MessageBox.Show("Пожалуйста, выберите товар для редактирования",
        //                      "Внимание",
        //                      MessageBoxButton.OK,
        //                      MessageBoxImage.Warning);
        //    }
        //}

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (listItems.SelectedItem is Items selectedItem)
            {
                var result = MessageBox.Show($"Удалить товар '{selectedItem.name_item}'?",
                                           "Подтверждение удаления",
                                           MessageBoxButton.YesNo,
                                           MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        AppConnect.model0db.Items.Remove(selectedItem);
                        AppConnect.model0db.SaveChanges();
                        LoadItems();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении: {ex.Message}",
                                      "Ошибка",
                                      MessageBoxButton.OK,
                                      MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите товар для удаления",
                                "Внимание",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
            }
        }

        private void ComboFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listItems.ItemsSource = FindItems();
        }

        private void TextSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            listItems.ItemsSource = FindItems();
        }

        private void ComboSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listItems.ItemsSource = FindItems();
        }


    }
}
