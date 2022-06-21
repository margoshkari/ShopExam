using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Text.Json;

namespace Shop.Pages
{
    public class AdminModel : PageModel
    {
        public string jsonprod = string.Empty;
        public string jsoncat = string.Empty;
        public List<Product> products = new List<Product>();
        public List<Category> categories = new List<Category>();
        public string Title = string.Empty;
        public void OnGet()
        {
            if(Request.Cookies["login"] == "admin")
            {
                products = SqlOperations.SelectStartProducts();
                jsonprod = JsonSerializer.Serialize<List<Product>>(products);
                Title = SqlOperations.Title;

                categories = SqlOperations.GetCategory();
                jsoncat = JsonSerializer.Serialize<List<Category>>(categories);
            }
        }
        public void OnPost(string category)
        {
            products = SqlOperations.SelectProducts(category);
            jsonprod = JsonSerializer.Serialize<List<Product>>(products);
            Title = SqlOperations.Title;

            categories = SqlOperations.GetCategory();
            jsoncat = JsonSerializer.Serialize<List<Category>>(categories);
        }
    }
}
