using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Text.Json;

namespace Shop.Pages
{
    public class IndexModel : PageModel
    {
        public string jsonprod = string.Empty;
        public string jsoncat = string.Empty;
        public List<Product> products = new List<Product>();
        public List<Category> categories = new List<Category>();
        public string Title = string.Empty;

        public void OnGet()
        {
            products = SqlOperations.SelectStartProducts();
            jsonprod = JsonSerializer.Serialize<List<Product>>(products);
            Title = SqlOperations.Title;

            categories = SqlOperations.GetCategory();
            jsoncat = JsonSerializer.Serialize<List<Category>>(categories);
        }
        public void OnPost(string category)
        {
            products = SqlOperations.SelectProducts(category);
            jsonprod = JsonSerializer.Serialize<List<Product>>(products);
            Title = SqlOperations.Title;

            categories = SqlOperations.GetCategory();
            jsoncat = JsonSerializer.Serialize<List<Category>>(categories);
        }
        public void OnPostView(string searchproduct)
        {
            List<Product> searchproducts = new List<Product>();
            products = SqlOperations.SelectExistProducts(searchproduct);
            foreach (var item in products)
            {
                if(item.ProductName.ToLower().Contains(searchproduct.ToLower()))
                {
                    searchproducts.Add(item);
                }
            }
            jsonprod = JsonSerializer.Serialize<List<Product>>(searchproducts);
            Title = "Search Result:";

            categories = SqlOperations.GetCategory();
            jsoncat = JsonSerializer.Serialize<List<Category>>(categories);
        }
    }
}
