using FoodRecipe.DAO;
using FoodRecipe.Db;
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
    /// Interaction logic for ListFood.xaml
    /// </summary>
    public partial class ListFood : Window
    {
        private BindingList<Food> _list = new BindingList<Food>();

        public ListFood()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //var result = DBUtils.read();
            
            _list = FoodDAO.GetAll();
            dataListView.ItemsSource = _list;

        }

        private void Button_Click_Fav(object sender, RoutedEventArgs e)
        {
            var favScreen = new FavoriteFood();
            favScreen.Show();
        }


        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_AddToFav(object sender, RoutedEventArgs e)
        {
            var id = ((Button)sender).Tag;
            bool result = FoodDAO.updateIsFavorite(id.ToString(), true);
            
            if(!result)
            {
                MessageBox.Show("Lỗi id không đúng", "Thông báo");
            }
            
            MessageBox.Show("Đã thêm vào danh sách yêu thích", "Thông báo");
        }

        private void Button_Click_Detail(object sender, RoutedEventArgs e)
        {
            var favScreen = new FavoriteFood();
            favScreen.Show();
        }
    }
}
