using FoodRecipe.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FoodRecipe.Db
{
    class DBUtils
    {
        public static List<Food> read()
        {
            XmlTextReader textReader = new XmlTextReader("../../Db/DB.xml");
            List<Food> data = new List<Food>();
            Food food = new Food();
            FoodStep foodStep = new FoodStep();

            while (textReader.Read())
            {
                if (textReader.NodeType == XmlNodeType.Element && textReader.Name == "name")
                {
                    food.Name = textReader.ReadElementString();
                }
                if (textReader.NodeType == XmlNodeType.Element && textReader.Name == "description")
                {
                   food.Description = textReader.ReadElementString();
                }
                if (textReader.NodeType == XmlNodeType.Element && textReader.Name == "video")
                {
                    food.VideoLink = textReader.ReadElementString();
                }
                if (textReader.NodeType == XmlNodeType.Element && textReader.Name == "steps")
                {

                }
                if (textReader.NodeType == XmlNodeType.Element && textReader.Name == "astep")
                {
                   
                }
                if (textReader.NodeType == XmlNodeType.Element && textReader.Name == "stepdescription")
                {
                    foodStep.DescriptionStep = textReader.ReadElementString();
                }
                if (textReader.NodeType == XmlNodeType.Element && textReader.Name == "stepvideo")
                {
                    foodStep.VideoStepLink = textReader.ReadElementString();

                    food.steps.Add(foodStep);
                    data.Add(food);
                }
            }

            return data;
        }

        public static void write(Food myFood)
        {
            XmlWriter xmlWriter = XmlWriter.Create("../../Db/DB.xml");

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("foods");

            xmlWriter.WriteStartElement("record");
            xmlWriter.WriteStartElement("name");
            xmlWriter.WriteString(myFood.Name);
            xmlWriter.WriteEndElement(); //</name>
            xmlWriter.WriteStartElement("description");
            xmlWriter.WriteString(myFood.Description);
            xmlWriter.WriteEndElement(); //</description>
            xmlWriter.WriteStartElement("video");
            xmlWriter.WriteString(myFood.VideoLink);
            xmlWriter.WriteEndElement(); //</video>
            xmlWriter.WriteStartElement("steps");

            foreach (FoodStep step in myFood.steps)
            {
                xmlWriter.WriteStartElement("astep");
                xmlWriter.WriteStartElement("stepdescription");
                xmlWriter.WriteString(step.DescriptionStep);
                xmlWriter.WriteEndElement(); //</stepdescription>
                xmlWriter.WriteStartElement("stepvideo");
                xmlWriter.WriteString(step.VideoStepLink);
                xmlWriter.WriteEndElement(); //</stepvideo>
                xmlWriter.WriteEndElement(); //</astep>
            }

            xmlWriter.WriteEndElement(); //</steps>

            xmlWriter.WriteEndDocument(); //</foods>
            xmlWriter.Close();
        }
    }
}
