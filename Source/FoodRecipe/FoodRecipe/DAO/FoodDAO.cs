using FoodRecipe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRecipe.DAO
{
    class FoodDAO
    {
        public static BindingList<Food> GetAll()
        {
            var result = new BindingList<Food>()
            {
                new Food() {CoverImage="https://post.healthline.com/wp-content/uploads/2020/09/healthy-eating-ingredients-1200x628-facebook-1200x628.jpg", Name="Food 1"},
                new Food() {CoverImage="https://post.healthline.com/wp-content/uploads/2020/09/healthy-eating-ingredients-1200x628-facebook-1200x628.jpg", Name="Food 2"},
                new Food() {CoverImage="https://post.healthline.com/wp-content/uploads/2020/09/healthy-eating-ingredients-1200x628-facebook-1200x628.jpg", Name="Food 2"},
                new Food() {CoverImage="https://post.healthline.com/wp-content/uploads/2020/09/healthy-eating-ingredients-1200x628-facebook-1200x628.jpg", Name="Food 2"},
                new Food() {CoverImage="https://post.healthline.com/wp-content/uploads/2020/09/healthy-eating-ingredients-1200x628-facebook-1200x628.jpg", Name="Food 2"},
                new Food() {CoverImage="https://post.healthline.com/wp-content/uploads/2020/09/healthy-eating-ingredients-1200x628-facebook-1200x628.jpg", Name="Food 2"},
                new Food() {CoverImage="https://post.healthline.com/wp-content/uploads/2020/09/healthy-eating-ingredients-1200x628-facebook-1200x628.jpg", Name="Food 2"},
                new Food() {CoverImage="https://post.healthline.com/wp-content/uploads/2020/09/healthy-eating-ingredients-1200x628-facebook-1200x628.jpg", Name="Food 2"},
                new Food() {CoverImage="https://post.healthline.com/wp-content/uploads/2020/09/healthy-eating-ingredients-1200x628-facebook-1200x628.jpg", Name="Food 2"},
                new Food() {CoverImage="https://post.healthline.com/wp-content/uploads/2020/09/healthy-eating-ingredients-1200x628-facebook-1200x628.jpg", Name="Food 2"},
            };
            return result;
        }
    }
}
