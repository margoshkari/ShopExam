using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text.Json;

namespace Shop.Pages
{
    public class CartModel : PageModel
    {
        SqlConnection sqlConnection = Connection.GetConnection();
        public string json = string.Empty;
        public List<Product> products = new List<Product>();
        public void OnGet()
        {
            string login = @Request.Cookies["login"];
            string idUser = string.Empty;
            if (login != null)
            {
                using (SqlCommand cmd = new SqlCommand($"SELECT * FROM [Users] WHERE [Users].[Login] = '{login}'", sqlConnection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            idUser = reader["idUser"].ToString();
                        }
                        reader.Close();
                    }
                }
            }
            if (idUser != string.Empty)
            {
                using (SqlCommand cmd = new SqlCommand($"SELECT * FROM [Cart] " +
                $"JOIN [Product] ON [Cart].[idProduct] = [Product].[idProduct] WHERE [Cart].[idUser] = {idUser} ", sqlConnection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            products.Add(new Product(reader["ImageName"].ToString(), reader["ProductName"].ToString(),
                                int.Parse(reader["Price"].ToString()), reader["Info"].ToString()));
                            products.Last().Count = int.Parse(reader["Count"].ToString());
                            products.Last().Price = (int.Parse(reader["Price"].ToString()) * int.Parse(reader["Count"].ToString()));
                            json = JsonSerializer.Serialize<List<Product>>(products);
                        }
                        reader.Close();
                    }
                }
            }
        }
        public IActionResult OnPost(int count, string productname, string username)
        {
            string idUser = string.Empty;
            string idProduct = string.Empty;
            if (username != null && productname != null)
            {
                using (SqlCommand cmd = new SqlCommand($"SELECT * FROM [Users] " +
                    $"JOIN [Product] ON [Product].[idProduct] = [Product].[idProduct] " +
                    $"WHERE [Users].[Login] = '{username}' AND [Product].[ProductName] = '{productname}'", sqlConnection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            idUser = reader["idUser"].ToString();
                            idProduct = reader["idProduct"].ToString();
                        }
                        reader.Close();
                    }
                }
            }
            if (idUser != string.Empty && idProduct != string.Empty)
            {
                using (SqlCommand cmd = new SqlCommand($"INSERT INTO [Cart] VALUES({count},{idProduct}, {idUser})", sqlConnection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            return Redirect("/Index");
        }
        public IActionResult OnPostDelete(string productname)
        {
            string idProduct = string.Empty;
            if(productname != null)
            {
                using (SqlCommand cmd = new SqlCommand($"SELECT * FROM [Cart] " +
                    $"JOIN [Product] ON [Cart].[idProduct] = [Product].[idProduct] " +
                    $"WHERE [Product].[ProductName] = '{productname}'", sqlConnection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            idProduct = reader["idProduct"].ToString();
                        }
                        reader.Close();
                    }
                }
            }
            if (idProduct != string.Empty)
            {
                using (SqlCommand cmd = new SqlCommand($"DELETE FROM [Cart] WHERE [Cart].[idProduct] = {idProduct}", sqlConnection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            return Redirect("/Cart");
        }
    }
}
