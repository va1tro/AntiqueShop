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
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private AntiqueShopEntities3 db = new AntiqueShopEntities3();
        public LoginPage()
        {
            InitializeComponent();
        }
        private void LoginBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoginPlaceholder.Visibility = string.IsNullOrEmpty(LoginBox.Text)
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordPlaceholder.Visibility = string.IsNullOrEmpty(PasswordBox.Password)
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var user = db.Users.FirstOrDefault(u =>
                u.login == LoginBox.Text && u.password == PasswordBox.Password);

            if (user != null)
            {
                MessageBox.Show($"Добро пожаловать, {user.first_name}!");
                NavigationService.Navigate(new ItemsPage());

                // Пример навигации на главную страницу после входа
                //NavigationService.Navigate(new MainMenuPage(user)); // передаём пользователя
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegisterPage()); // Убедись, что эта страница существует
        }
    }
}
