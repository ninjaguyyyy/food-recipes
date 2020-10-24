using FoodRecipe.Models.AddFoodModels;
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

namespace FoodRecipe.Screens.AddFoodScreens
{
    /// <summary>
    /// Interaction logic for AddSteps_1.xaml
    /// </summary>
    public partial class AddSteps_1 : UserControl
    {
        public AddSteps_1()
        {
            InitializeComponent();
        }

        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Application.Current.MainWindow;
            if (parentWindow.GetType() == typeof(MainWindow))
            {
                (parentWindow as MainWindow).DataContext = new AddGeneralInfoModel();
            }
        }

        private void Button_Click_Submit(object sender, RoutedEventArgs e)
        {

        }
    }
}
