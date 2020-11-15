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
        public static int GetLengthAll()
        {
            var result = 0;

            XDocument xdocument = XDocument.Load("../../Db/DB.xml");
            IEnumerable<XElement> foodsElements = xdocument.Root.Elements();
            result = foodsElements.Count();

            return result;
        }

        public static int GetLengthFavs()
        {
            var result = 0;

            XDocument xdocument = XDocument.Load("../../Db/DB.xml");
            IEnumerable<XElement> foods = xdocument.Root.Elements()
                .Where(el => Boolean.Parse(el.Element("isFavorite").Value));
            result = foods.Count();

            return result;
        }

        public static BindingList<Food> GetAll(int perPage, int page, string sortBy)
        {
            var result = new BindingList<Food>();

            int skipValue = page == 1 ? 0 : (page - 1) * perPage;


            XDocument xdocument = XDocument.Load("../../Db/DB.xml");
            IEnumerable<XElement> foodsElements = xdocument.Root.Elements();


            if (sortBy == "az")
            {
                foodsElements = foodsElements.OrderBy(s => s.Element("name").Value);
            }
            
            if (sortBy == "za")
            {
                foodsElements = foodsElements.OrderByDescending(s => s.Element("name").Value);
            }

            if (sortBy == "newold")
            {
                foodsElements = foodsElements.OrderBy(s => Convert.ToDateTime(s.Element("createdAt").Value));
            }

            if (sortBy == "oldnew")
            {
                foodsElements = foodsElements.OrderByDescending(s => Convert.ToDateTime(s.Element("createdAt").Value));
            }

            foodsElements = foodsElements.Skip(skipValue).Take(perPage);

            result = ConvertListXmlElementToFoods(foodsElements);

            return result;
        }

        public static BindingList<Food> SearchFoods(string searchKey)
        {
            var result = new BindingList<Food>();

            XDocument xdocument = XDocument.Load("../../Db/DB.xml");
            IEnumerable<XElement> foodsElements = xdocument.Root.Elements();
            foodsElements = foodsElements.Where(e => e.Element("name").Value == searchKey);

            result = ConvertListXmlElementToFoods(foodsElements);

            return result;
        }
        public static BindingList<Food> ConvertListXmlElementToFoods(IEnumerable<XElement> listElement)
        {
            var result = new BindingList<Food>();

            foreach (var xelement in listElement)
            {
                var foodItem = new Food();
                foodItem.Id = xelement.Element("id").Value;
                foodItem.Name = xelement.Element("name").Value;
                foodItem.Description = xelement.Element("description").Value;
                foodItem.ThumbnailPath = xelement.Element("thumbnailPath").Value;
                foodItem.IsFavorite = Boolean.Parse(xelement.Element("isFavorite").Value);

                result.Add(foodItem);
            }

            return result;
        }

        public static BindingList<Food> GetFavorites(int perPage, int page)
        {
            var result = new BindingList<Food>();

            int skipValue = page == 1 ? 0 : (page - 1) * perPage;

            XDocument xdocument = XDocument.Load("../../Db/DB.xml");
            IEnumerable<XElement> foods = xdocument.Root.Elements()
                .Where(el => Boolean.Parse(el.Element("isFavorite").Value))
                .Skip(skipValue).Take(perPage);

            result = ConvertListXmlElementToFoods(foods);
            

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