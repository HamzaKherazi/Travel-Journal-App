using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelJournalDataAccessLayer;

namespace TravelJournalBusinessLayer
{
    public class clsCategory
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }

        public clsCategory()
        {
            this.CategoryID = -1;
            this.CategoryName = "";
        }

        public clsCategory(int categoryID, string categoryName)
        {
            this.CategoryID = categoryID;
            this.CategoryName = categoryName;
        }

        static public clsCategory Find(int categoryID)
        {
            string CategoryName = "";

            if(CategoryData.GetCategoryByID(categoryID, ref CategoryName))
            {
                return new clsCategory(categoryID, CategoryName);
            }
            else
            {
                return null;
            }
        }

        static public clsCategory Find(string CategoryName)
        {
            int CategoryID = -1;

            if (CategoryData.GetCategoryByName(CategoryName, ref CategoryID))
            {
                return new clsCategory(CategoryID, CategoryName);
            }
            else
            {
                return null;
            }
        }

        static public DataTable CategoriesList()
        {
            return CategoryData.GetAllCategories();
        }

    }
}
