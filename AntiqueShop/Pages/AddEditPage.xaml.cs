using AntiqueShop.AppData;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
        private string _imageFileName = ""; // Имя файла изображения

        public AddEditPage(Items selectedItem = null)
        {
            InitializeComponent();

            if (selectedItem != null)
            {
                _currentItem = selectedItem;
                _imageFileName = selectedItem.image ?? "";
            }

            DataContext = _currentItem;
            LoadComboBoxes();
            LoadItemData();
        }

        private void LoadComboBoxes()
        {
            try
            {
                using (var db = new AntiqueShopEntities3())
                {
                    // Загрузка данных для выпадающих списков
                    cbSupplier.ItemsSource = db.Suppliers.ToList();
                    cbCategory.ItemsSource = db.Categories.ToList();
                    cbMaterial.ItemsSource = db.Materials.ToList();
                    cbCountry.ItemsSource = db.Origin_countries.ToList();
                    cbStatus.ItemsSource = db.Statuses.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadItemData()
        {
            if (_currentItem.id_item != 0) // Редактирование существующего товара
            {
                // Загрузка изображения
                imgItem.Source = new BitmapImage(new Uri(_currentItem.CurrentPhoto, UriKind.RelativeOrAbsolute));

                // Установка значений в ComboBox
                SelectComboBoxItem(cbSupplier, _currentItem.id_supplier);
                SelectComboBoxItem(cbCategory, _currentItem.id_category);
                SelectComboBoxItem(cbMaterial, _currentItem.id_material);
                SelectComboBoxItem(cbCountry, _currentItem.id_origin_country);
                SelectComboBoxItem(cbStatus, _currentItem.id_status);

                // Установка значений в ComboBox для состояния и подлинности
                if (!string.IsNullOrEmpty(_currentItem.condition))
                    cbCondition.SelectedItem = cbCondition.Items.Cast<ComboBoxItem>()
                        .FirstOrDefault(i => i.Content.ToString() == _currentItem.condition);

                if (!string.IsNullOrEmpty(_currentItem.authenticity))
                    cbAuthenticity.SelectedItem = cbAuthenticity.Items.Cast<ComboBoxItem>()
                        .FirstOrDefault(i => i.Content.ToString() == _currentItem.authenticity);
            }
            else // Новый товар
            {
                imgItem.Source = new BitmapImage(new Uri("/Images/picture.jpg", UriKind.Relative));
                dpArrivalDate.SelectedDate = DateTime.Today;
            }
        }

        private void SelectComboBoxItem(ComboBox comboBox, int? id)
        {
            if (id.HasValue)
                comboBox.SelectedValue = id;
        }

        private void BtnSelectImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png|All files (*.*)|*.*",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    // Получаем имя файла без пути
                    _imageFileName = System.IO.Path.GetFileName(openFileDialog.FileName);

                    // Копируем изображение в папку Images проекта
                    string destPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", _imageFileName);
                    File.Copy(openFileDialog.FileName, destPath, true);

                    // Обновляем изображение в интерфейсе
                    imgItem.Source = new BitmapImage(new Uri(destPath));
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при загрузке изображения: {ex.Message}", "Ошибка",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            // Валидация наименования
            if (string.IsNullOrWhiteSpace(tbName.Text))
                errors.AppendLine("Укажите наименование товара");

            // Валидация года
            if (!int.TryParse(tbYear.Text, out int year) || year < 1000 || year > DateTime.Now.Year)
                errors.AppendLine("Укажите корректный год изготовления (1000-текущий год)");

            // Валидация поставщика
            if (cbSupplier.SelectedItem == null)
                errors.AppendLine("Выберите поставщика");

            // Валидация цен
            if (!decimal.TryParse(tbPurchasePrice.Text.Replace('.', ','), out decimal purchasePrice) || purchasePrice <= 0)
                errors.AppendLine("Укажите корректную цену покупки (число больше 0)");

            if (!decimal.TryParse(tbSellingPrice.Text.Replace('.', ','), out decimal sellingPrice) || sellingPrice <= 0)
                errors.AppendLine("Укажите корректную цену продажи (число больше 0)");

            // Валидация даты
            if (dpArrivalDate.SelectedDate == null)
                errors.AppendLine("Укажите дату поступления");

            // Валидация выпадающих списков
            if (cbCategory.SelectedItem == null)
                errors.AppendLine("Выберите категорию");

            if (cbMaterial.SelectedItem == null)
                errors.AppendLine("Выберите материал");

            if (cbCountry.SelectedItem == null)
                errors.AppendLine("Выберите страну происхождения");

            if (cbStatus.SelectedItem == null)
                errors.AppendLine("Выберите статус");

            if (cbCondition.SelectedItem == null)
                errors.AppendLine("Укажите состояние товара");

            if (cbAuthenticity.SelectedItem == null)
                errors.AppendLine("Укажите подлинность товара");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                // Заполнение данных товара
                _currentItem.name_item = tbName.Text;
                _currentItem.year = year;
                _currentItem.id_supplier = (int)((dynamic)cbSupplier.SelectedItem).id_supplier;
                _currentItem.purchase_price = purchasePrice;
                _currentItem.selling_price = sellingPrice;
                _currentItem.arrival_date = dpArrivalDate.SelectedDate.Value;
                _currentItem.id_category = (int)((dynamic)cbCategory.SelectedItem).id_category;
                _currentItem.id_material = (int)((dynamic)cbMaterial.SelectedItem).id_material;
                _currentItem.id_origin_country = (int)((dynamic)cbCountry.SelectedItem).id_origin_country;
                _currentItem.id_status = (int)((dynamic)cbStatus.SelectedItem).id_status;
                _currentItem.condition = (cbCondition.SelectedItem as ComboBoxItem)?.Content.ToString();
                _currentItem.authenticity = (cbAuthenticity.SelectedItem as ComboBoxItem)?.Content.ToString();
                _currentItem.image = _imageFileName;

                using (var db = new AntiqueShopEntities3())
                {
                    if (_currentItem.id_item == 0)
                    {
                        db.Items.Add(_currentItem);
                    }
                    else
                    {
                        db.Entry(_currentItem).State = System.Data.Entity.EntityState.Modified;
                    }

                    db.SaveChanges();
                }

                MessageBox.Show("Данные успешно сохранены!", "Успех",
                              MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}\n\nПодробности: {ex.InnerException?.Message}",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
