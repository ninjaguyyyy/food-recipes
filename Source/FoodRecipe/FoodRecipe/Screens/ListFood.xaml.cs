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
        private int perPage = 4;
        private int page = 1;
        private int totalPage = 1;

        public ListFood()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //var result = DBUtils.read();

            int totalFood = FoodDAO.GetLengthAll();
            totalPage = (totalFood / perPage) + 1;
            
            _list = FoodDAO.GetAll(perPage, page);
            dataListView.ItemsSource = _list;

            var firstCreatedButton = createPagingButton(true, "1");
            pagingStackPanel.Children.Add(firstCreatedButton);

            for (int i = 2; i <= totalPage; i++)
            {
                var createdButton = createPagingButton(false, i.ToString());

                pagingStackPanel.Children.Add(createdButton);
            }
        }

        private void pagingButton_Click(object sender, RoutedEventArgs e)
        {
            resetActivePagingButton();
            ((Button)sender).Background = Brushes.Red;

            var pageSelected = ((Button)sender).Content;
            
            _list = FoodDAO.GetAll(perPage, int.Parse(pageSelected.ToString()));
            dataListView.ItemsSource = _list;
        }

        private Button createPagingButton(bool isFirst, string content)
        {
            var result = new Button();

            result.Content = content;
            Thickness margin = result.Margin;
            margin.Left = 10;
            margin.Right = 10;
            result.Margin = margin;
            result.Cursor = Cursors.Hand;
            result.Background = isFirst? Brushes.Red: Brushes.Transparent;
            result.Click += pagingButton_Click;

            return result;
        }

        private void resetActivePagingButton()
        {
            var pagingButtons = pagingStackPanel.Children;

            foreach(var el in pagingButtons)
            {
                ((Button)el).Background = Brushes.Transparent;
            }
            
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
