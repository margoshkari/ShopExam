using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Shop.Pages
{
    public class RegisterModel : PageModel
    {
        public string login { get; set; } = "";
        public void OnGet()
        {
        }
        public void OnPost(string username, string password, string email)
        {
            if (!SqlOperations.isUserExist(username))
            {
                SqlOperations.UserRegister(username, password, email);
                login = "notexist";
            }
            else
            {
                login = "exist";
            }
        }
    }
}
