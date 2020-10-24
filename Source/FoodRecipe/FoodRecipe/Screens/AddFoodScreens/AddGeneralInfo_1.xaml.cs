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
using FoodRecipe;
using FoodRecipe.Models.AddFoodModels;
using FoodRecipe.DTO;

namespace FoodRecipe.Screens.AddFoodScreens
{
    /// <summary>
    /// Interaction logic for AddGeneralInfo_1.xaml
    /// </summary>
    public partial class AddGeneralInfo_1 : UserControl
    {
        static AddGeneralInfo_1 _obj;

        public static AddGeneralInfo_1 Instance
        {
            get
            {
                if (_obj == null)
                {
                    _obj = new AddGeneralInfo_1();
                }
                return _obj;
            }
        }

        public AddGeneralInfo_1()
        {
            InitializeComponent();
        }

        private Food myFood = new Food();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //binding data 
            this.DataContext = myFood;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //change user control
            Window parentWindow = Application.Current.MainWindow;
            if (parentWindow.GetType() == typeof(MainWindow))
            {
                (parentWindow as MainWindow).DataContext = new AddStepsModel();
            }
        }
    }
}
