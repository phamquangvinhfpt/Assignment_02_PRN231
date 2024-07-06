using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text;
using BusinessObject.Models;
using eBookStore.Service;

namespace eBookStore.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;

        public LoginModel(IHttpClientFactory httpClientFactory, IConfiguration configuration, IAuthService authService)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _authService = authService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        private class LoginResult
        {
            public int StatusCode { get; set; }
            public bool Status { get; set; }
            public string Message { get; set; }
            public LoginData Data { get; set; }
        }

        private class LoginData
        {
            public string Token { get; set; }
            public User User { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var client = _httpClientFactory.CreateClient();
            var apiUrl = _configuration["ApiBaseUrl"] + "/api/auth/login";

            var loginData = new { email = Input.Email, password = Input.Password };
            var content = new StringContent(JsonSerializer.Serialize(loginData), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var loginResult = JsonSerializer.Deserialize<LoginResult>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                _authService.SetAuthenticationStatus(true, loginResult.Data.User.EmailAddress, loginResult.Data.User.RoleId == 1 ? true : false);
                HttpContext.Session.SetString("Token", loginResult.Data.Token);
                return RedirectToPage("/Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return Page();
            }
        }

        public void OnGet()
        {
            if(_authService.IsAuthenticated)
            {
                Response.Redirect("/Index");
            }
        }
    }
}
