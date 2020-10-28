using FoodRecipe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FoodRecipe.DAO
{
    class FoodDAO
    {
        public static BindingList<Food> GetAll()
        {
            var result = new BindingList<Food>();

            XDocument xdocument = XDocument.Load("../../Db/DB.xml");
            IEnumerable<XElement> foods = xdocument.Root.Elements();
            foreach (var foodEl in foods)
            {
                var foodItem = new Food();
                foodItem.Id = foodEl.Element("id").Value;
                foodItem.Name = foodEl.Element("name").Value;
                foodItem.Description = foodEl.Element("description").Value;
                foodItem.ThumbnailPath = foodEl.Element("thumbnailPath").Value;
                foodItem.IsFavorite = Boolean.Parse(foodEl.Element("isFavorite").Value);

                result.Add(foodItem);
            }

            return result;
        }

        public static BindingList<Food> GetFavorites()
        {
            var result = new BindingList<Food>();

            XDocument xdocument = XDocument.Load("../../Db/DB.xml");
            IEnumerable<XElement> foods = xdocument.Root.Elements();
            foreach (var foodEl in foods)
            {
                if (Boolean.Parse(foodEl.Element("isFavorite").Value))
                {
                    var foodItem = new Food();
                    foodItem.Id = foodEl.Element("id").Value;
                    foodItem.Name = foodEl.Element("name").Value;
                    foodItem.Description = foodEl.Element("description").Value;
                    foodItem.ThumbnailPath = foodEl.Element("thumbnailPath").Value;
                    foodItem.IsFavorite = Boolean.Parse(foodEl.Element("isFavorite").Value);

                    result.Add(foodItem);
                }
                   
            }

            return result;
        }

        public static Food getById(string id)
        {
            dynamic result = null;

            XDocument xdocument = XDocument.Load("../../Db/DB.xml");
            IEnumerable<XElement> foods = xdocument.Root.Elements();
            foreach(var foodEl in foods)
            {
                if(id == foodEl.Element("id").Value)
                {
                    var findedFood = new Food();
                    findedFood.Id = foodEl.Element("id").Value;
                    findedFood.Name = foodEl.Element("name").Value;
                    findedFood.Description = foodEl.Element("description").Value;
                    findedFood.ThumbnailPath = foodEl.Element("thumbnailPath").Value;
                    findedFood.IsFavorite = Boolean.Parse(foodEl.Element("isFavorite").Value);

                    result = findedFood;
                    break;
                }
            }

            return result;
        }

        public static bool updateIsFavorite(string id, bool isFav)
        {
            var result = false;

            XDocument xdocument = XDocument.Load("../../Db/DB.xml");
            IEnumerable<XElement> foods = xdocument.Root.Elements();
            foreach (var foodEl in foods)
            {
                if (id == foodEl.Element("id").Value)
                {
                    foodEl.SetElementValue("isFavorite", isFav.ToString());

                    result = true;
                    break;
                }
            }

            xdocument.Save("../../Db/DB.xml");
            return result;
        }
    }
}