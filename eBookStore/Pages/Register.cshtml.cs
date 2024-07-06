using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text;
using eBookStore.Service;
using BusinessObject.Models;

namespace eBookStore.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;

        public RegisterModel(IHttpClientFactory httpClientFactory, IConfiguration configuration, IAuthService authService)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _authService = authService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public string EmailAddress { get; set; }
            public string Password { get; set; }
            public string ConfirmPassword { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Input.Password != Input.ConfirmPassword)
            {
                ModelState.AddModelError(string.Empty, "The password and confirmation password do not match.");
                return Page();
            }

            var client = _httpClientFactory.CreateClient();
            var apiUrl = _configuration["ApiBaseUrl"] + "/api/users";

            var user = new User
            {
                EmailAddress = Input.EmailAddress,
                Password = Input.Password,
                RoleId = 2 // User
            };

            var content = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    // Registration successful
                    return RedirectToPage("./Login");
                }
                else
                {
                    // Registration failed
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, $"Registration failed: {errorMessage}");
                    return Page();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
                return Page();
            }
        }

        public void OnGet()
        {
            if (_authService.IsAuthenticated)
            {
                Response.Redirect("/Index");
            }
        }
    }
}
