using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ceng382_25_26_202011037.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public CredentialInput Input { get; set; } = default!;

        public bool LoginFailed { get; set; }

        public void OnGet() { }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            if (Input.Username == "admin" && Input.Password == "admin")
            {
                HttpContext.Session.SetString("User", Input.Username);
                return RedirectToPage("/Index");
            }

            LoginFailed = true;
            return Page();
        }
    }

    public class CredentialInput
    {
        [Required(ErrorMessage = "Kullanıcı adı gerekli.")]
        public string Username { get; set; } = default!;

        [Required(ErrorMessage = "Şifre gerekli.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = default!;
    }
}
