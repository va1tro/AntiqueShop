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
    /// Логика взаимодействия для UserInfoPage.xaml
    /// </summary>
    public partial class UserInfoPage : Page
    {
        public UserInfoPage()
        {
            InitializeComponent();
            LoadUserInfo();
        }

        private void LoadUserInfo()
        {
            var user = AppConnect.CurrentUser;

            tbFullName.Text = $"{user.last_name} {user.first_name} {user.middle_name}";
            tbLogin.Text = user.login;
            tbEmail.Text = user.email;
            tbRole.Text = user.Role.name_role;
            tbPreferences.Text = string.IsNullOrWhiteSpace(user.preferences)
                ? "Не указаны"
                : user.preferences;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
