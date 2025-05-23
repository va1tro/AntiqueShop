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
    /// Логика взаимодействия для RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        public RegisterPage()
        {
            InitializeComponent();
        }
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginBox.Text.Trim();
            string password = PasswordBox.Password.Trim();
            string confirmPassword = ConfirmPasswordBox.Password.Trim();
            string firstName = FirstNameBox.Text.Trim();
            string lastName = LastNameBox.Text.Trim();
            string middleName = MiddleNameBox.Text.Trim();
            string email = EmailBox.Text.Trim();
            string preferences = PreferencesBox.Text.Trim();

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Пожалуйста, заполните обязательные поля: логин и пароль.");
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Пароли не совпадают.");
                return;
            }

            using (var db = new AntiqueShopEntities3())
            {
                if (db.Users.Any(u => u.login == login))
                {
                    MessageBox.Show("Пользователь с таким логином уже существует.");
                    return;
                }

                var user = new Users
                {
                    login = login,
                    password = password, // Хеширование рекомендуется для реальных проектов
                    first_name = firstName,
                    last_name = lastName,
                    middle_name = middleName,
                    email = email,
                    preferences = preferences,
                    id_role = 2 
                };

                db.Users.Add(user);  
                db.SaveChanges();
            }



            MessageBox.Show("Регистрация прошла успешно!");
            NavigationService.GoBack(); // Возвращаемся на страницу входа
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender == LoginBox)
                LoginPlaceholder.Visibility = string.IsNullOrWhiteSpace(LoginBox.Text) ? Visibility.Visible : Visibility.Collapsed;
            else if (sender == FirstNameBox)
                FirstNamePlaceholder.Visibility = string.IsNullOrWhiteSpace(FirstNameBox.Text) ? Visibility.Visible : Visibility.Collapsed;
            else if (sender == LastNameBox)
                LastNamePlaceholder.Visibility = string.IsNullOrWhiteSpace(LastNameBox.Text) ? Visibility.Visible : Visibility.Collapsed;
            else if (sender == MiddleNameBox)
                MiddleNamePlaceholder.Visibility = string.IsNullOrWhiteSpace(MiddleNameBox.Text) ? Visibility.Visible : Visibility.Collapsed;
            else if (sender == EmailBox)
                EmailPlaceholder.Visibility = string.IsNullOrWhiteSpace(EmailBox.Text) ? Visibility.Visible : Visibility.Collapsed;
            else if (sender == PreferencesBox)
                PreferencesPlaceholder.Visibility = string.IsNullOrWhiteSpace(PreferencesBox.Text) ? Visibility.Visible : Visibility.Collapsed;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender == PasswordBox)
                PasswordPlaceholder.Visibility = string.IsNullOrWhiteSpace(PasswordBox.Password) ? Visibility.Visible : Visibility.Collapsed;
            else if (sender == ConfirmPasswordBox)
                ConfirmPasswordPlaceholder.Visibility = string.IsNullOrWhiteSpace(ConfirmPasswordBox.Password) ? Visibility.Visible : Visibility.Collapsed;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Назад (например, навигация на LoginPage)
            NavigationService?.Navigate(new LoginPage());
        }
    }
}
