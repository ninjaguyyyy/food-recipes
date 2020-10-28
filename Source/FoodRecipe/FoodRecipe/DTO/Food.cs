using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRecipe.DTO
{
    class Food
    {
        public Food()
        {
            Name = "";
            Description = "";
            ThumbnailPath = "";
        }

        public string Id { get; set;  }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ThumbnailPath { get; set; }
        public string VideoLink { get; set; }
        public bool IsFavorite { get; set; }

        public List<FoodStep> steps = new List<FoodStep>();
    }

    class FoodStep
    {
        public string DescriptionStep { get; set; }
        public string VideoStepLink { get; set; }
        public string ImageStepPath { get; set; }
    }
}
