using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Shop.Pages
{
    public class RegisterModel : PageModel
    {
        public string login { get; set; } = "";
        public string useremail { get; set; } = "";
        public void OnGet()
        {
        }
        public void OnPost(string username, string password, string email)
        {
            if (!SqlOperations.isUserExist(username))
                login = "notexist";
            else
                login = "exist";

            if (!SqlOperations.isEmailExist(email))
                useremail = "notexist";
            else
                useremail = "exist";

            if (login == "notexist" && useremail == "notexist")
                SqlOperations.UserRegister(username, password, email);
        }
    }
}
