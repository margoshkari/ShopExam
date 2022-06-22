using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Text.Json;

namespace Shop.Pages
{
    public class CartModel : PageModel
    {
        public string json = string.Empty;
        public List<Product> products = new List<Product>();
        public void OnGet()
        {
            string login = @Request.Cookies["login"];
            string idUser = SqlOperations.GetUserId(login);

            if (idUser != string.Empty)
            {
                products = SqlOperations.GetCartProduct(idUser);
                json = JsonSerializer.Serialize<List<Product>>(products);
            }
        }
        public IActionResult OnPost(int count, string productname, string username)
        {
            string idUser = string.Empty;
            string idProduct = string.Empty;
            if (username != null && productname != null && count > 0)
            {
                idUser = SqlOperations.GetUserId(username);
                idProduct = SqlOperations.GetProductId(productname);
            }
            else
            {
                return Redirect("/Index");
            }
            if (idUser != string.Empty && idProduct != string.Empty)
            {
               
                if (SqlOperations.isProductExist(idProduct, idUser))
                {
                    count += SqlOperations.ProductCount(idProduct, idUser);
                    SqlOperations.UpdateProduct(idProduct, idUser, count);
                }
                else
                    SqlOperations.AddCartProduct(count, idProduct, idUser);
            }

            return Redirect("/Index");
        }
        public IActionResult OnPostDelete(string productname, string username)
        {
            string idUser = string.Empty;
            string idProduct = string.Empty;
            if (username != null && productname != null)
            {
                idUser = SqlOperations.GetUserId(username);
                idProduct = SqlOperations.GetProductId(productname);
            }
            if (idProduct != string.Empty && idUser != string.Empty)
            {
                SqlOperations.DeleteCartProduct(idProduct, idUser);
            }

            return Redirect("/Cart");
        }
        public IActionResult OnPostView(string username)
        {
            string idUser = string.Empty; 
            if (username != null)
            {
                idUser = SqlOperations.GetUserId(username);
            }
            if (idUser != string.Empty)
            {
                SqlOperations.DeleteAllCartProduct(idUser);
            }

            return Redirect("/Cart");
        }

    }
}
