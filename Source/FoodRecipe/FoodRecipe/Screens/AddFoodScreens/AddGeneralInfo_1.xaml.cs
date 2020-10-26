﻿using System;
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
using System.Xml;
using FoodRecipe.Db;
using Microsoft.Win32;
using System.IO;

namespace FoodRecipe.Screens.AddFoodScreens
{
    /// <summary>
    /// Interaction logic for AddGeneralInfo_1.xaml
    /// </summary>
    public partial class AddGeneralInfo_1 : UserControl
    {
        /*static AddGeneralInfo_1 _obj;

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
        }*/

        private Random _rand = new Random();

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
            if (myFood.Name.Equals("") || myFood.Description.Equals("") || myFood.ThumbnailPath.Equals("") || foodSteps.Count == 0)
            {
                NotiLabel.Opacity = 100;
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
                System.IO.File.Copy(myFood.steps[i].ImageStepPath, newImagePath);
                myFood.steps[i].ImageStepPath = newImagePath;
            }

            DBUtils.write(myFood);
        }

        private List<FoodStep> foodSteps = new List<FoodStep>();
        private int step = 0;
        private string path_temp;

        private void Button_Add_Step(object sender, RoutedEventArgs e)
        {
            foodSteps.Add ( new FoodStep
            {
                DescriptionStep = DescriptionStep.Text,
                VideoStepLink = VideoStepLink.Text,
                ImageStepPath = path_temp
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
    }
}