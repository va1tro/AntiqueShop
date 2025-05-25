using AntiqueShop.AppData;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AntiqueShop.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {
        private Items _currentItem = new Items();
        private string _selectedImagePath = null;
        private bool _isEditMode = false;

        public AddEditPage(Items selectedItem = null)
        {
            InitializeComponent();

            if (selectedItem != null)
            {
                _currentItem = selectedItem;
                _isEditMode = true;
            }

            DataContext = _currentItem;

            cbSupplier.ItemsSource = AppConnect.model0db.Suppliers.ToList();
            cbCategory.ItemsSource = AppConnect.model0db.Categories.ToList();
            cbMaterial.ItemsSource = AppConnect.model0db.Materials.ToList();
            cbCountry.ItemsSource = AppConnect.model0db.Origin_countries.ToList();
            cbStatus.ItemsSource = AppConnect.model0db.Statuses.ToList();

            if (_isEditMode)
            {
                tbName.Text = _currentItem.name_item;
                tbYear.Text = _currentItem.year?.ToString();
                tbPurchasePrice.Text = _currentItem.purchase_price?.ToString();
                tbSellingPrice.Text = _currentItem.selling_price?.ToString();
                dpArrivalDate.SelectedDate = _currentItem.arrival_date;

                cbSupplier.SelectedItem = _currentItem.Suppliers;
                cbCategory.SelectedItem = _currentItem.Categories;
                cbMaterial.SelectedItem = _currentItem.Materials;
                cbCountry.SelectedItem = _currentItem.Origin_countries;
                cbStatus.SelectedItem = _currentItem.Statuses;

                cbCondition.Text = _currentItem.condition;
                cbAuthenticity.Text = _currentItem.authenticity;

                imgItem.Source = new BitmapImage(new Uri(_currentItem.CurrentPhoto, UriKind.RelativeOrAbsolute));
            }
        }

        private void BtnSelectImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Изображения (*.jpg;*.png)|*.jpg;*.png";

            if (ofd.ShowDialog() == true)
            {
                _selectedImagePath = ofd.FileName;
                imgItem.Source = new BitmapImage(new Uri(_selectedImagePath));

                _currentItem.image = System.IO.Path.GetFileName(_selectedImagePath);
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            // Валидация
            if (string.IsNullOrWhiteSpace(tbName.Text) || cbSupplier.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, заполните обязательные поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _currentItem.name_item = tbName.Text;
            _currentItem.condition = (cbCondition.SelectedItem as ComboBoxItem)?.Content.ToString();
            _currentItem.authenticity = (cbAuthenticity.SelectedItem as ComboBoxItem)?.Content.ToString();

            _currentItem.year = int.TryParse(tbYear.Text, out int year) ? year : (int?)null;
            _currentItem.purchase_price = decimal.TryParse(tbPurchasePrice.Text, out decimal purchase) ? purchase : (decimal?)null;
            _currentItem.selling_price = decimal.TryParse(tbSellingPrice.Text, out decimal sell) ? sell : (decimal?)null;
            _currentItem.arrival_date = dpArrivalDate.SelectedDate;

            _currentItem.id_supplier = (cbSupplier.SelectedItem as Suppliers)?.id_supplier;
            _currentItem.id_category = (cbCategory.SelectedItem as Categories)?.id_category;
            _currentItem.id_material = (cbMaterial.SelectedItem as Materials)?.id_material;
            _currentItem.id_origin_country = (cbCountry.SelectedItem as Origin_countries)?.id_origin_country;
            _currentItem.id_status = (cbStatus.SelectedItem as Statuses)?.id_status;

            // Копирование изображения
            if (_selectedImagePath != null)
            {
                string fileName = System.IO.Path.GetFileName(_selectedImagePath);
                string destPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", fileName);

                try
                {
                    File.Copy(_selectedImagePath, destPath, true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка копирования изображения: " + ex.Message);
                }
            }

            // Сохранение
            if (!_isEditMode)
                AppConnect.model0db.Items.Add(_currentItem);

            try
            {
                AppConnect.model0db.SaveChanges();
                MessageBox.Show("Информация сохранена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
