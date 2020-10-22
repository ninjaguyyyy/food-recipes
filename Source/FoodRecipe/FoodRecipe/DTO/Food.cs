using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRecipe.DTO
{
    class Food
    {
        public string Name { get; set; }
        public string Description { get; set; }
        //hinh anh thanh qua

        public FoodStep[] steps;
    }

    class FoodStep
    {
        public string DescriptionStep { get; set; }
        //danh sach hinh anh minh hoa
        public string VideoStepLink { get; set; }
    }
}
