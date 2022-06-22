using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Shop.Pages
{
    public class CategoryModel : PageModel
    {
        public bool isAdd = true;
        public string categoryadd { get; set; } = "";
        public string categorydelete { get; set; } = "";
        public void OnGet(string isadd)
        {
            if (isadd == "add")
                isAdd = true;
            else
                isAdd = false;
        }
        public void OnPost(string categoryname)
        {
            if (isAdd)
            {
                if (!SqlOperations.isCategoryExist(categoryname))
                {
                    categoryadd = "notexist";
                    SqlOperations.AddCategory(categoryname);
                }
                else
                    categoryadd = "exist";
            }
        }
        public void OnPostDelete(string categoryname)
        {
            if (!SqlOperations.isCategoryExist(categoryname))
            {
                categorydelete = "notexist";
            }
            else
            {
                SqlOperations.DeleteCategory(categoryname);
                categorydelete = "exist";
            }
        }
    }
}
