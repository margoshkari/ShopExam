using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Shop.Pages
{
    public class CategoryModel : PageModel
    {
        public bool isAdd = true;
        public void OnGet(string isadd)
        {
            if (isadd == "add")
                isAdd = true;
            else
                isAdd = false;
        }
        public IActionResult OnPost(string categoryname)
        {
            if (isAdd)
            {
                if (SqlOperations.AddCategory(categoryname))
                    return Redirect("/Admin");
            }
            return Redirect("/Category");
        }
        public IActionResult OnPostDelete(string categoryname)
        {
            if (SqlOperations.DeleteCategory(categoryname))
                return Redirect("/Admin");
            return Redirect("/Category");
        }
    }
}
