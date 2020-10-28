using FoodRecipe.DAO;
using FoodRecipe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace FoodRecipe.Screens
{
    /// <summary>
    /// Interaction logic for FavoriteFood.xaml
    /// </summary>
    public partial class FavoriteFood : Window
    {
        private BindingList<Food> _list = new BindingList<Food>();
        public FavoriteFood()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _list = FoodDAO.GetAll();
            dataListView.ItemsSource = _list;

        }
    }
}
