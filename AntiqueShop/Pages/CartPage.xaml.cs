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
    /// Логика взаимодействия для CartPage.xaml
    /// </summary>
    public partial class CartPage : Page
    {
        public CartPage()
        {
            InitializeComponent();
            LoadCartItems();
        }
        private void LoadCartItems()
        {
            var cartItems = AppConnect.model0db.Cart
                .Where(c => c.id_user == AppConnect.CurrentUser.id_user && c.is_active == true)
                .ToList();

            CartList.ItemsSource = cartItems;
            UpdateTotalPrice(cartItems);
        }

        private void UpdateTotalPrice(System.Collections.Generic.List<Cart> cartItems)
        {
            decimal total = cartItems.Sum(c => c.Items.selling_price.GetValueOrDefault() * c.quantity);
            tbTotal.Text = $"Итого: {total:C}";
        }

        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).DataContext is Cart cart)
            {
                AppConnect.model0db.Cart.Remove(cart);
                AppConnect.model0db.SaveChanges();
                LoadCartItems();
            }
        }

        private void BtnConfirmOrder_Click(object sender, RoutedEventArgs e)
        {
            var cartItems = AppConnect.model0db.Cart
                .Where(c => c.id_user == AppConnect.CurrentUser.id_user && c.is_active == true)
                .ToList();

            if (cartItems.Count == 0)
            {
                MessageBox.Show("Корзина пуста");
                return;
            }

            foreach (var cartItem in cartItems)
            {
                AppConnect.model0db.Sales.Add(new Sales
                {
                    id_user = cartItem.id_user,
                    id_item = cartItem.id_item,
                    sale_date = DateTime.Now,
                    final_price = cartItem.Items.selling_price
                });

                cartItem.is_active = false; // помечаем как оформленные
            }

            AppConnect.model0db.SaveChanges();
            MessageBox.Show("Заказ оформлен!");

            LoadCartItems();
        }

        private void QuantityChanged(object sender, TextChangedEventArgs e)
        {
            if ((sender as TextBox).DataContext is Cart cart && int.TryParse((sender as TextBox).Text, out int qty))
            {
                cart.quantity = qty > 0 ? qty : 1;
                AppConnect.model0db.SaveChanges();
                LoadCartItems();
            }
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
