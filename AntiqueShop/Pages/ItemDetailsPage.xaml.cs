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
    /// Логика взаимодействия для ItemDetailsPage.xaml
    /// </summary>
    public partial class ItemDetailsPage : Page
    {
        public ItemDetailsPage(Items selectedItem)
        {
            InitializeComponent();

            ItemName.Text = selectedItem.name_item;
            ItemYear.Text = selectedItem.year.ToString();
            ItemCondition.Text = selectedItem.condition;
            ItemAuthenticity.Text = selectedItem.authenticity;
            ItemPurchasePrice.Text = $"{selectedItem.purchase_price} руб.";
            ItemSellingPrice.Text = $"{selectedItem.selling_price} руб.";
            ItemArrivalDate.Text = selectedItem.arrival_date?.ToShortDateString();

            if (selectedItem.image != null && selectedItem.image.Length > 0)
            {
                using (ItemImage.Source = new BitmapImage(new Uri(selectedItem.image, UriKind.RelativeOrAbsolute)));
                {
                    var bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.StreamSource = stream;
                    bitmap.EndInit();
                    ItemImage.Source = bitmap;
                }
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
