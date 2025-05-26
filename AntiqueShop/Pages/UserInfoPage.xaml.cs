using AntiqueShop.AppData;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для UserInfoPage.xaml
    /// </summary>
    public partial class UserInfoPage : Page
    {
        private Users currentUser;

        public class BoolToVisibilityConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                bool isEmpty = string.IsNullOrWhiteSpace(value?.ToString());
                return isEmpty ? Visibility.Visible : Visibility.Collapsed;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                return DependencyProperty.UnsetValue;
            }
        }
        public UserInfoPage()
        {
            InitializeComponent();
            currentUser = AppConnect.CurrentUser;
            LoadUserData();
        }

        private void LoadUserData()
        {
            FirstNameBox.Text = currentUser.first_name;
            LastNameBox.Text = currentUser.last_name;
            MiddleNameBox.Text = currentUser.middle_name;
            EmailBox.Text = currentUser.email;
            PreferencesBox.Text = currentUser.preferences;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            // Подтверждение
            var result = MessageBox.Show("Вы уверены, что хотите сохранить изменения?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes)
                return;

            var user = AppConnect.CurrentUser;

            // Проверка старого пароля
            if (!string.IsNullOrWhiteSpace(NewPasswordBox.Password))
            {
                if (OldPasswordBox.Password != user.password)
                {
                    MessageBox.Show("Неверный старый пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Смена пароля
                user.password = NewPasswordBox.Password;
            }

            // Валидация email
            if (!IsValidEmail(EmailBox.Text))
            {
                MessageBox.Show("Некорректный email", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Сохраняем изменения в текущего пользователя
            user.first_name = FirstNameBox.Text;
            user.last_name = LastNameBox.Text;
            user.middle_name = MiddleNameBox.Text;
            user.email = EmailBox.Text;
            user.preferences = PreferencesBox.Text;

            try
            {
                AppConnect.model0db.SaveChanges();
                MessageBox.Show("Данные успешно сохранены", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void ChangePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            string oldPass = OldPasswordBox.Password;
            string newPass = NewPasswordBox.Password;
            string confirmPass = ConfirmPasswordBox.Password;

            if (string.IsNullOrWhiteSpace(oldPass) || string.IsNullOrWhiteSpace(newPass))
            {
                MessageBox.Show("Введите старый и новый пароли.", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (currentUser.password != oldPass)
            {
                MessageBox.Show("Старый пароль неверен.", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (newPass != confirmPass)
            {
                MessageBox.Show("Новый пароль и подтверждение не совпадают.", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            currentUser.password = newPass;

            try
            {
                AppConnect.model0db.SaveChanges();
                MessageBox.Show("Пароль успешно изменён.", "Успех",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                OldPasswordBox.Clear();
                NewPasswordBox.Clear();
                ConfirmPasswordBox.Clear();
            }
            catch
            {
                MessageBox.Show("Ошибка при смене пароля.", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void FirstNameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            FirstNamePlaceholder.Visibility = string.IsNullOrWhiteSpace(FirstNameBox.Text)
                ? Visibility.Visible : Visibility.Collapsed;
        }

        private void LastNameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            LastNamePlaceholder.Visibility = string.IsNullOrWhiteSpace(LastNameBox.Text)
                ? Visibility.Visible : Visibility.Collapsed;
        }

        private void MiddleNameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            MiddleNamePlaceholder.Visibility = string.IsNullOrWhiteSpace(MiddleNameBox.Text)
                ? Visibility.Visible : Visibility.Collapsed;
        }

        private void EmailBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            EmailPlaceholder.Visibility = string.IsNullOrWhiteSpace(EmailBox.Text)
                ? Visibility.Visible : Visibility.Collapsed;
        }

        private void PreferencesBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            PreferencesPlaceholder.Visibility = string.IsNullOrWhiteSpace(PreferencesBox.Text)
                ? Visibility.Visible : Visibility.Collapsed;
        }

        private void NewPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            NewPasswordPlaceholder.Visibility = string.IsNullOrWhiteSpace(NewPasswordBox.Password)
                ? Visibility.Visible : Visibility.Collapsed;
        }

        private void ConfirmPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ConfirmPasswordPlaceholder.Visibility = string.IsNullOrWhiteSpace(ConfirmPasswordBox.Password)
                ? Visibility.Visible : Visibility.Collapsed;
        }
        private void OldPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            OldPasswordPlaceholder.Visibility = string.IsNullOrWhiteSpace(OldPasswordBox.Password)
                ? Visibility.Visible : Visibility.Collapsed;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
