using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace Shop.Pages
{
    public class RegisterModel : PageModel
    {
        SqlConnection sqlConnection = Connection.GetConnection();
        public void OnGet()
        {
        }
        public IActionResult OnPost(string username, string password, string email)
        {
            password = ComputeSha256Hash(password);
            using (SqlCommand cmd = new SqlCommand($"INSERT INTO [Users] VALUES('{username}','{password}', '{email}', 1)", sqlConnection))
            {
                cmd.ExecuteNonQuery();
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
