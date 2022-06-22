using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Shop.Pages
{
    public class LoginModel : PageModel
    {
        public string login { get; set; } = "";
        public void OnPost(string username, string password)
        {
            if (SqlOperations.UserLogin(username, password))
            {
                Response.Cookies.Append("login", username);
                login = "correct";
            }
            else
                login = "uncorrect";
        }
    }
}
