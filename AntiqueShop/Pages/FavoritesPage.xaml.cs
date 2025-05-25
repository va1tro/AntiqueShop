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
    /// Логика взаимодействия для FavoritesPage.xaml
    /// </summary>
    public partial class FavoritesPage : Page
    {
        private Users currentUser;

        public FavoritesPage()
        {
            InitializeComponent();
            currentUser = AppConnect.CurrentUser; // Убедись, что в AppConnect есть это свойство
            LoadFavorites();
        }

        private void LoadFavorites()
        {
            var user = AppConnect.CurrentUser;

            if (user == null)
            {
                MessageBox.Show("Ошибка: пользователь не авторизован");
                return;
            }

            var favorites = AppConnect.model0db.Favorites
                .Where(f => f.id_user == user.id_user && f.Items != null)
                .Select(f => f.Items)
                .ToList();

            listFavorites.ItemsSource = favorites;

            tbCounter.Text = favorites.Count > 0
                ? $"Найдено избранных товаров: {favorites.Count}"
                : "Избранные товары не найдены";
        }

        private void RemoveFromFavorites_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as FrameworkElement)?.DataContext is Items selectedItem)
            {
                var favoriteEntry = AppConnect.model0db.Favorites
                    .FirstOrDefault(f => f.id_item == selectedItem.id_item && f.id_user == currentUser.id_user);

                if (favoriteEntry != null)
                {
                    AppConnect.model0db.Favorites.Remove(favoriteEntry);
                    AppConnect.model0db.SaveChanges();
                    LoadFavorites();
                    MessageBox.Show($"Товар '{selectedItem.name_item}' удалён из избранного.");
                }
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
