using FoodRecipe.Db;
using FoodRecipe.DTO;
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
using System.Windows.Shapes;

namespace FoodRecipe.Screens
{
    /// <summary>
    /// Interaction logic for AddRecipe.xaml
    /// </summary>
    public partial class AddRecipe : Window
    {
        public AddRecipe()
        {
            InitializeComponent();
        }

        private Random _rand = new Random();

        private Food myFood = new Food();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //binding data 
            //this.DataContext = myFood;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            myFood.Name = txtName.Text;
            myFood.VideoLink = txtVideoLink.Text;
            myFood.Description = txtDescription.Text;

            if (myFood.Name.Equals("") || myFood.Description.Equals("") || myFood.ThumbnailPath.Equals("") || foodSteps.Count == 0)
            {
                MessageBox.Show("Vui lòng điền đầy đủ các trường bắt buộc");
                return;
            }

            myFood.steps = foodSteps;

            //create a new folder in /Pictues
            string rootFloder = "../Db/Pictures";
            int index = _rand.Next(100);
            //DateTime localDate = DateTime.Now;
            //DescriptionStep.Text = localDate.ToString();
            string childFolder = index.ToString();
            string pathString = System.IO.Path.Combine(rootFloder, childFolder);
            while (Directory.Exists(pathString))
            {
                index = _rand.Next(100);
                childFolder = index.ToString();
                pathString = System.IO.Path.Combine(rootFloder, childFolder);
            }
            Directory.CreateDirectory(pathString);

            string newImagePath = pathString + "/thumbnail.jpg";

            File.Copy(myFood.ThumbnailPath, newImagePath);
            myFood.ThumbnailPath = newImagePath;

            for (int i = 0; i < myFood.steps.Count; i++)
            {
                newImagePath = pathString + $"/{i}.jpg";
                if (myFood.steps[i].ImageStepPath.Count > 0)
                {
                    if (myFood.steps[i].ImageStepPath[0] != null)
                    {
                        System.IO.File.Copy(myFood.steps[i].ImageStepPath[0], newImagePath);
                        myFood.steps[i].ImageStepPath[0] = newImagePath;
                    }
                }
            }

            DBUtils.write(myFood);

            MessageBox.Show("Lưu thành công!");
        }

        private List<FoodStep> foodSteps = new List<FoodStep>();
        private int step = 0;
        private string path_temp;

        private void Button_Add_Step(object sender, RoutedEventArgs e)
        {
            List<string> pathTemp = new List<string>();
            pathTemp.Add(path_temp);
            foodSteps.Add(new FoodStep
            {
                DescriptionStep = DescriptionStep.Text,
                VideoStepLink = VideoStepLink.Text,
                ImageStepPath = pathTemp
            });

            StepsBox.Text += StepLabel.Content + ": " + DescriptionStep.Text;
            StepsBox.Text += Environment.NewLine;

            DescriptionStep.Text = "";
            VideoStepLink.Text = "";

            step++;

            StepLabel.Content = $"Bước {step + 1}";
        }

        private void Upload_Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            var o = op.ShowDialog();
            if (o == true)
            {
                Image.Source = new BitmapImage(new Uri(op.FileName));
                Image.Opacity = 1;
                Image.MaxWidth = 200;
            }

            myFood.ThumbnailPath = op.FileName;
        }

        private void StepImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            var o = op.ShowDialog();
            if (o == true)
            {
                Step_Image.Source = new BitmapImage(new Uri(op.FileName));
                Step_Image.Width = 90;
            }

            path_temp = op.FileName;
        }

        private void Button_Click_Fav(object sender, RoutedEventArgs e)
        {
            var favScreen = new FavoriteFood();
            favScreen.Show();
            this.Close();
        }

        private void Button_Click_List(object sender, RoutedEventArgs e)
        {
            var listScreen = new ListFood();
            listScreen.Show();
            this.Close();
        }
    }
}

