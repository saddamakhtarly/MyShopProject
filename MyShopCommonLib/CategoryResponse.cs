using System;
using System.Collections.Generic;
using System.Text;

namespace MyShopCommonLib
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class CategoryResponse : Response
    {
        public CategoryResponse()
        {
            CategoryList = new List<Category>();
        }
        public List<Category> CategoryList { get; set; }
    }
    public class SaveCategoryResponse : Response
    {
        
    }
}
