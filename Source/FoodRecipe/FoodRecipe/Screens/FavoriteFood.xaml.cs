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
            _list = FoodDAO.GetFavorites();
            dataListView.ItemsSource = _list;

        }

        private void Button_Click_Detail(object sender, RoutedEventArgs e)
        {
            var favScreen = new FavoriteFood();
            favScreen.Show();
        }

        private void Button_Click_RemoveFav(object sender, RoutedEventArgs e)
        {
            var id = ((Button)sender).Tag;
            bool result = FoodDAO.updateIsFavorite(id.ToString(), false);

            if (!result)
            {
                MessageBox.Show("Lỗi id không đúng", "Thông báo");
            }

            MessageBox.Show("Đã xóa khỏi danh sách yêu thích", "Thông báo");

            this.Close();
            var updatedScreen = new FavoriteFood();
            updatedScreen.Show();
        }
    }
}
