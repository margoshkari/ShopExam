using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace Shop.Pages
{
    public class LoginModel : PageModel
    {
        SqlConnection sqlConnection = Connection.GetConnection();
        public IActionResult OnPost(string username, string password)
        {
            password = ComputeSha256Hash(password);
            using (SqlCommand cmd = new SqlCommand($"SELECT * FROM [Users] WHERE [Login] = '{username}' AND [HeshPassword] = '{password}'", sqlConnection))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Response.Cookies.Append("login", username);
                        return Redirect("/Index");
                    }
                }
            }
            return Redirect("/Login");
        }
        private string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
