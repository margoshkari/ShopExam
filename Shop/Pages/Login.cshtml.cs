using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Shop.Pages
{
    public class LoginModel : PageModel
    {
        public IActionResult OnPost(string username, string password)
        {
            if (SqlOperations.UserLogin(username, password))
            {
                Response.Cookies.Append("login", username);
                return Redirect("/Index");
            }
            return Redirect("/Login");
        }
    }
}
