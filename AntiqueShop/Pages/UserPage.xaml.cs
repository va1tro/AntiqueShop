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
    /// Логика взаимодействия для UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        public UserPage()
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
            var categories = new List<string> { "Все" };
            categories.AddRange(AppConnect.model0db.Categories.Select(c => c.name_category));
            ComboFilter.ItemsSource = categories;
            ComboFilter.SelectedIndex = 0;

            ComboSort.SelectedIndex = 0;
        }

        private void UpdateCounter(int count)
        {
            tbCounter.Text = count > 0 ? $"Найдено товаров: {count}" : "Товары не найдены";
        }

        private List<Items> FindItems()
        {
            var items = AppConnect.model0db.Items.ToList();

            // Поиск
            if (!string.IsNullOrWhiteSpace(TextSearch.Text))
            {
                items = items.Where(i => i.name_item.ToLower().Contains(TextSearch.Text.ToLower())).ToList();
            }

            // Фильтрация по категории
            if (ComboFilter.SelectedIndex > 0)
            {
                var selectedCategory = ComboFilter.SelectedItem.ToString();
                items = items.Where(i => i.Categories.name_category == selectedCategory).ToList();
            }

            // Сортировка
            switch (ComboSort.SelectedIndex)
            {
                case 1:
                    items = items.OrderBy(i => i.year).ToList();
                    break;
                case 2:
                    items = items.OrderByDescending(i => i.year).ToList();
                    break;
                case 3:
                    items = items.OrderBy(i => i.purchase_price).ToList();
                    break;
                case 4:
                    items = items.OrderByDescending(i => i.purchase_price).ToList();
                    break;
                case 5:
                    items = items.OrderByDescending(i => i.arrival_date).ToList();
                    break;
                case 6:
                    items = items.OrderBy(i => i.arrival_date).ToList();
                    break;
            }

            UpdateCounter(items.Count);
            return items;
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

        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as FrameworkElement)?.DataContext is Items selectedItem)
            {
                var user = AppConnect.CurrentUser;
                if (user == null)
                {
                    MessageBox.Show("Необходима авторизация.");
                    return;
                }

                // Проверка, есть ли уже товар в корзине
                var existingCart = AppConnect.model0db.Cart
                    .FirstOrDefault(c => c.id_user == user.id_user && c.id_item == selectedItem.id_item && c.is_active == true);

                if (existingCart != null)
                {
                    existingCart.quantity += 1; // увеличиваем количество
                }
                else
                {
                    var cart = new Cart
                    {
                        id_user = user.id_user,
                        id_item = selectedItem.id_item,
                        quantity = 1,
                        added_date = DateTime.Now,
                        is_active = true
                    };
                    AppConnect.model0db.Cart.Add(cart);
                }

                AppConnect.model0db.SaveChanges();
                MessageBox.Show($"Товар '{selectedItem.name_item}' добавлен в корзину.");
            }
        }

        private void AddToFavorites_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as FrameworkElement)?.DataContext is Items selectedItem)
            {
                var user = AppConnect.CurrentUser;
                if (user == null)
                {
                    MessageBox.Show("Необходима авторизация.");
                    return;
                }

                // Проверка, есть ли товар в избранном
                var existingFavorite = AppConnect.model0db.Favorites
                    .FirstOrDefault(f => f.id_user == user.id_user && f.id_item == selectedItem.id_item);

                if (existingFavorite != null)
                {
                    MessageBox.Show("Товар уже в избранном.");
                    return;
                }

                var favorite = new Favorites
                {
                    id_user = user.id_user,
                    id_item = selectedItem.id_item
                };

                AppConnect.model0db.Favorites.Add(favorite);
                AppConnect.model0db.SaveChanges();

                MessageBox.Show($"Товар '{selectedItem.name_item}' добавлен в избранное.");
            }
        }

        private void Cart_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CartPage());
        }

        private void Favorites_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new FavoritesPage());
        }
        private void MyOrders_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MyOrdersPage());
        }
        private void UserInfo_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UserInfoPage());
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LoginPage());
        }
    }
}
