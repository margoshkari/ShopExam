using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Shop.Pages
{
    public class ProductsModel : PageModel
    {
        public bool isAdd = true;
        public void OnGet(string isadd)
        {
            if (isadd == "add")
                isAdd = true;
            else
                isAdd = false;
        }
        public IActionResult OnPost(string productname, int price, string imagename, string info, string categoryname)
        {
            string idCategory = string.Empty;
            if (isAdd)
            {
                idCategory = SqlOperations.GetCategoryId(categoryname);
                if (idCategory != string.Empty)
                {
                    if (SqlOperations.AddProduct(productname, price, imagename, info, idCategory))
                        return Redirect("/Admin");
                }
            }
            return Redirect("/Products");
        }
        public IActionResult OnPostDelete(string productname)
        {
                string idProduct = string.Empty;
                if (productname != null)
                {
                    idProduct = SqlOperations.GetProductId(productname);
                }
                if (idProduct != string.Empty)
                {
                    if (SqlOperations.DeleteProduct(idProduct))
                        return Redirect("/Admin");
                }

            return Redirect("/Products");
        }
    }
}
