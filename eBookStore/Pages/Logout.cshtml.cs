using eBookStore.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eBookStore.Pages
{
    public class LogoutModel : PageModel
    {
        private readonly IAuthService _authService;

        public LogoutModel(IAuthService authService)
        {
            _authService = authService;
        }

        public IActionResult OnPost()
        {
            _authService.ClearAuthenticationStatus();
            HttpContext.Session.Remove("Token");
            return RedirectToPage("/Index");
        }

        public void OnGet()
        {
        }
    }
}
