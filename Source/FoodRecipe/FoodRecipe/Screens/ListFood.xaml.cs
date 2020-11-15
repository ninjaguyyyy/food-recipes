using FoodRecipe.DAO;
using FoodRecipe.Db;
using FoodRecipe.DTO;
using FoodRecipe.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
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
        private int perPage;
        private int page = 1;
        private int totalFoods;
        private string sortBy;
        private string search;

        public ListFood()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            perPage = int.Parse(config.AppSettings.Settings["PerPage"].Value);

            totalFoods = FoodDAO.GetLengthAll();
            int totalPage = (totalFoods % perPage == 0)? (totalFoods / perPage): (totalFoods / perPage) + 1;
            perPageTextbox.Text = perPage.ToString();

            _list = FoodDAO.GetAll(perPage, page, sortBy);
            dataListView.ItemsSource = _list;

            createPagingUI(totalPage);
        }

        private void createPagingUI(int totalPage)
        {
            pagingStackPanel.Children.Clear();

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
            ((Button)sender).Background = Brushes.LightBlue;

            var pageSelected = ((Button)sender).Content;
            
            _list = FoodDAO.GetAll(perPage, int.Parse(pageSelected.ToString()), sortBy);
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
            Thickness padding = result.Padding;
            padding.Left = 5;
            padding.Right = 5;
            padding.Top = 5;
            padding.Bottom = 5;
            result.Padding = padding;
            result.Cursor = Cursors.Hand;
            result.Background = isFirst? Brushes.LightBlue: Brushes.Transparent;
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

        private void resetInitialPagingButton()
        {
            resetActivePagingButton();
            var firstButton = pagingStackPanel.Children.OfType<Button>().FirstOrDefault();
            if (firstButton != null) { 
                firstButton.Background = Brushes.LightBlue;
            }
        }

        private void Button_Click_Fav(object sender, RoutedEventArgs e)
        {
            var favScreen = new FavoriteFood();
            favScreen.Show();
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
            var detailScreen = new RecipeDetail();
            detailScreen.Show();
            this.Close();
        }


        private void Button_Click_ChangePerPage(object sender, RoutedEventArgs e)
        {

            string enteredPerPage = perPageTextbox.Text;
            if(int.Parse(enteredPerPage) > 12)
            {
                MessageBox.Show("Chương trình chỉ hỗ trợ tối đa 12 sản phẩm trên 1 trang", "Thông báo");
                return;
            }
            perPage = int.Parse(enteredPerPage);
            int totalPage = (totalFoods % perPage == 0) ? (totalFoods / perPage) : (totalFoods / perPage) + 1;

            _list = FoodDAO.GetAll(perPage, page, sortBy);
            dataListView.ItemsSource = _list;

            createPagingUI(totalPage);

            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["PerPage"].Value = enteredPerPage;
            config.Save(ConfigurationSaveMode.Minimal);
        }

        private void sortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sortBy = ((ComboBoxItem)sortComboBox.SelectedItem).Tag.ToString();

            _list = FoodDAO.GetAll(perPage, page, sortBy);
            dataListView.ItemsSource = _list;

            if (pagingStackPanel != null) {
                resetInitialPagingButton();
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            string enteredSearch = searchTextBox.Text.Trim();

            MessageBox.Show(SearchHelper.ConvertToUnSign(enteredSearch));

            //BindingList<Food> result = FoodDAO.SearchFoods(enteredSearch);
            //foreach (Food i in result)
            //{
            //    MessageBox.Show(i.Name);
            //}
        }
    }
}
