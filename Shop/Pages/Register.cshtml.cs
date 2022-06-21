using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Shop.Pages
{
    public class RegisterModel : PageModel
    {
        public void OnGet()
        {
        }
        public IActionResult OnPost(string username, string password, string email)
        {
            SqlOperations.UserRegister(username, password, email);
            return Redirect("/Login");
        }
    }
}
