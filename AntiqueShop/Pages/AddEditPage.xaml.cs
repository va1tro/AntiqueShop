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
        private Items _currentItem;
        private AntiqueShopEntities3 _context = new AntiqueShopEntities3();

        public AddEditPage(Items selectedItem = null)
        {
            InitializeComponent();

            if (selectedItem != null)
                _currentItem = _context.Items.FirstOrDefault(i => i.id_item == selectedItem.id_item);
            else
                _currentItem = new Items();

            // Если редактирование, то запоминаем выбранный товар
            ////_currentItem = selectedItem ?? new Items();

            // Привязка данных к ComboBox'ам
            cbCategory.ItemsSource = _context.Categories.ToList();
            cbCategory.DisplayMemberPath = "name_category";
            cbCategory.SelectedValuePath = "id_category";

            cbMaterial.ItemsSource = _context.Materials.ToList();
            cbMaterial.DisplayMemberPath = "name_material";
            cbMaterial.SelectedValuePath = "id_material";

            cbSupplier.ItemsSource = _context.Suppliers.ToList();
            cbSupplier.DisplayMemberPath = "name_supplier";
            cbSupplier.SelectedValuePath = "id_supplier";

            cbStatus.ItemsSource = _context.Statuses.ToList();
            cbStatus.DisplayMemberPath = "name_status";
            cbStatus.SelectedValuePath = "id_status";

            cbOriginCountry.ItemsSource = _context.Origin_countries.ToList();
            cbOriginCountry.DisplayMemberPath = "name_country";
            cbOriginCountry.SelectedValuePath = "id_origin_country";

            // Если редактируем — заполним поля
            if (selectedItem != null)
            {
                tbName.Text = _currentItem.name_item;
                tbYear.Text = _currentItem.year?.ToString();
                tbCondition.Text = _currentItem.condition;
                tbAuthenticity.Text = _currentItem.authenticity;
                tbPurchasePrice.Text = _currentItem.purchase_price?.ToString();
                tbSellingPrice.Text = _currentItem.selling_price?.ToString();
                dpArrivalDate.SelectedDate = _currentItem.arrival_date;
                tbImage.Text = _currentItem.image;

                cbCategory.SelectedValue = _currentItem.id_category;
                cbMaterial.SelectedValue = _currentItem.id_material;
                cbSupplier.SelectedValue = _currentItem.id_supplier;
                cbStatus.SelectedValue = _currentItem.id_status;
                cbOriginCountry.SelectedValue = _currentItem.id_origin_country;
            }
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Простейшая валидация
                if (string.IsNullOrWhiteSpace(tbName.Text) || cbCategory.SelectedItem == null)
                {
                    MessageBox.Show("Заполните обязательные поля (название и категория)");
                    return;
                }

                // Присваиваем значения
                _currentItem.name_item = tbName.Text;
                _currentItem.year = int.TryParse(tbYear.Text, out int year) ? year : (int?)null;
                _currentItem.condition = tbCondition.Text;
                _currentItem.authenticity = tbAuthenticity.Text;
                _currentItem.purchase_price = decimal.TryParse(tbPurchasePrice.Text, out decimal purchasePrice) ? purchasePrice : (decimal?)null;
                _currentItem.selling_price = decimal.TryParse(tbSellingPrice.Text, out decimal sellingPrice) ? sellingPrice : (decimal?)null;
                _currentItem.arrival_date = dpArrivalDate.SelectedDate;
                _currentItem.image = tbImage.Text;

                _currentItem.id_category = cbCategory.SelectedValue as int?;
                _currentItem.id_material = cbMaterial.SelectedValue as int?;
                _currentItem.id_supplier = cbSupplier.SelectedValue as int?;
                _currentItem.id_status = cbStatus.SelectedValue as int?;
                _currentItem.id_origin_country = cbOriginCountry.SelectedValue as int?;

                // Добавление или редактирование
                if (_currentItem.id_item == 0)
                    _context.Items.Add(_currentItem); // только если новый
                if (_currentItem.id_item != 0)
                {
                    var oldItem = _context.Items.AsNoTracking().FirstOrDefault(i => i.id_item == _currentItem.id_item);
                    var logList = new List<Item_logs>();

                    void AddLog(string field, string oldVal, string newVal)
                    {
                        if (oldVal != newVal)
                        {
                            logList.Add(new Item_logs
                            {
                                id_item = _currentItem.id_item,
                                changed_field = field,
                                old_value = oldVal,
                                new_value = newVal,
                                change_date = DateTime.Now
                            });
                        }
                    }

                    AddLog("name_item", oldItem.name_item, _currentItem.name_item);
                    AddLog("year", oldItem.year?.ToString(), _currentItem.year?.ToString());
                    AddLog("condition", oldItem.condition, _currentItem.condition);
                    AddLog("authenticity", oldItem.authenticity, _currentItem.authenticity);
                    AddLog("purchase_price", oldItem.purchase_price?.ToString(), _currentItem.purchase_price?.ToString());
                    AddLog("selling_price", oldItem.selling_price?.ToString(), _currentItem.selling_price?.ToString());
                    AddLog("arrival_date", oldItem.arrival_date?.ToString("yyyy-MM-dd"), _currentItem.arrival_date?.ToString("yyyy-MM-dd"));
                    AddLog("image", oldItem.image, _currentItem.image);
                    AddLog("id_category", oldItem.id_category?.ToString(), _currentItem.id_category?.ToString());
                    AddLog("id_material", oldItem.id_material?.ToString(), _currentItem.id_material?.ToString());
                    AddLog("id_supplier", oldItem.id_supplier?.ToString(), _currentItem.id_supplier?.ToString());
                    AddLog("id_status", oldItem.id_status?.ToString(), _currentItem.id_status?.ToString());
                    AddLog("id_origin_country", oldItem.id_origin_country?.ToString(), _currentItem.id_origin_country?.ToString());

                    _context.Item_logs.AddRange(logList);
                }

                _context.SaveChanges();
                MessageBox.Show("Данные успешно сохранены!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                // Возврат назад
                NavigationService.GoBack();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении: " + ex.Message);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}