using FoodRecipe.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace FoodRecipe.Db
{
    class DBUtils
    {
        public static List<Food> read()
        {
            List<Food> data = new List<Food>();
            XDocument xDocument = XDocument.Load("../../Db/DB.xml");
            IEnumerable<XElement> foods = xDocument.Root.Elements();
            foreach (var foodEl in foods)
            {
                var foodItem = new Food();
                foodItem.Name = foodEl.Element("name").Value;
                foodItem.Description = foodEl.Element("description").Value;
                foodItem.ThumbnailPath = foodEl.Element("thumbnailPath").Value;
                foodItem.IsFavorite = Boolean.Parse(foodEl.Element("isFavorite").Value);

                data.Add(foodItem);
            }

            return data;
        }

        public static void write(Food myFood)
        {
            List<Food> listFood = read();
            listFood.Add(myFood);

            XmlWriter xmlWriter = XmlWriter.Create("../../Db/DB.xml");

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("foods");
            foreach (Food f in listFood)
            {
                xmlWriter.WriteStartElement("record");
                xmlWriter.WriteStartElement("name");
                xmlWriter.WriteString(f.Name);
                xmlWriter.WriteEndElement(); //</name>
                xmlWriter.WriteStartElement("description");
                xmlWriter.WriteString(f.Description);
                xmlWriter.WriteEndElement(); //</description>
                xmlWriter.WriteStartElement("thumbnailPath");
                xmlWriter.WriteString(f.ThumbnailPath);
                xmlWriter.WriteEndElement(); //</thumbnailPath>
                xmlWriter.WriteStartElement("video");
                xmlWriter.WriteString(f.VideoLink);
                xmlWriter.WriteEndElement(); //</video>
                xmlWriter.WriteStartElement("isFavorite");
                bool isFavourite = f.IsFavorite;
                xmlWriter.WriteString(isFavourite.ToString());
                xmlWriter.WriteEndElement(); //</isFavorite>
                xmlWriter.WriteStartElement("steps");

                foreach (FoodStep step in f.steps)
                {
                    xmlWriter.WriteStartElement("astep");
                    xmlWriter.WriteStartElement("stepdescription");
                    xmlWriter.WriteString(step.DescriptionStep);
                    xmlWriter.WriteEndElement(); //</stepdescription>
                    xmlWriter.WriteStartElement("stepimagepath");
                    xmlWriter.WriteString(step.ImageStepPath);
                    xmlWriter.WriteEndElement(); //</stepimagepath>
                    xmlWriter.WriteStartElement("stepvideo");
                    xmlWriter.WriteString(step.VideoStepLink);
                    xmlWriter.WriteEndElement(); //</stepvideo>
                    xmlWriter.WriteEndElement(); //</astep>
                }

                xmlWriter.WriteEndElement(); //</steps>
            }

            xmlWriter.WriteEndDocument(); //</foods>

            xmlWriter.Close();
        }
    }
}
