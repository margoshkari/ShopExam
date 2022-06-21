using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Shop.Pages
{
    public class IndexModel : PageModel
    {

        SqlConnection sqlConnection = Connection.GetConnection();
        public string json = string.Empty;
        public List<Product> products = new List<Product>();
        public string Title = string.Empty;

        public void OnGet()
        {
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM [Product] JOIN [Category] ON " +
                "[Product].[idCategory] = [Category].[idCategory] WHERE [Product].[idCategory] = 1", sqlConnection))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new Product(reader["ImageName"].ToString(), reader["ProductName"].ToString(), 
                            int.Parse(reader["Price"].ToString()), reader["Info"].ToString()));
                        json = JsonSerializer.Serialize<List<Product>>(products);
                        Title = reader["CategoryName"].ToString();
                    }
                }
            }
        }
        public void OnPost(string category)
        {
            using (SqlCommand cmd = new SqlCommand($"SELECT * FROM [Product] JOIN [Category] ON " +
                $"[Product].[idCategory] = [Category].[idCategory] WHERE [Product].[idCategory] = {category}", sqlConnection))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new Product(reader["ImageName"].ToString(), reader["ProductName"].ToString(),
                            int.Parse(reader["Price"].ToString()), reader["Info"].ToString()));
                        json = JsonSerializer.Serialize<List<Product>>(products);
                        Title = reader["CategoryName"].ToString();
                    }
                }
            }
        }
    }
}
