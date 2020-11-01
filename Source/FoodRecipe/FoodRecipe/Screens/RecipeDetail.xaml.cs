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
using System.Windows.Shapes;

namespace FoodRecipe.Screens
{
    /// <summary>
    /// Interaction logic for RecipeDetail.xaml
    /// </summary>
    public partial class RecipeDetail : Window
    {
        public RecipeDetail()
        {
            InitializeComponent();
        }

        private void Button_Click_Fav(object sender, RoutedEventArgs e)
        {
            var favScreen = new FavoriteFood();
            favScreen.Show();
        }


        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_List(object sender, RoutedEventArgs e)
        {

        }
    }
}
