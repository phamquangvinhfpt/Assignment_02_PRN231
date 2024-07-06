using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace eBookStore.Pages
{
    public class AuthorsModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;

        public AuthorsModel(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
        }

        [BindProperty]
        public Author Author { get; set; }
        public List<Author> Authors { get; set; }
        public List<string> Cities { get; set; }
        public int TotalCount { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SearchTerm { get; set; }
        public string OrderBy { get; set; }

        public async Task OnGetAsync(string searchTerm, string orderBy, int? pageNumber)
        {
            SearchTerm = searchTerm;
            OrderBy = orderBy;
            CurrentPage = pageNumber ?? 1;
            await LoadAuthorsAsync(searchTerm, orderBy, (CurrentPage - 1) * PageSize, PageSize);
            await LoadCitiesAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadAuthorsAsync();
                await LoadCitiesAsync();
                return Page();
            }

            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
            var response = await client.PostAsJsonAsync($"{_configuration["ApiBaseUrl"]}/odata/Authors", Author);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error saving the author.");
                await LoadAuthorsAsync();
                await LoadCitiesAsync();
                return Page();
            }
        }

        private async Task LoadCitiesAsync()
        {
            Cities = new List<string> { "New York", "Los Angeles", "Chicago", "Houston", "Phoenix" };
        }
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < (int)Math.Ceiling((double)TotalCount / PageSize);

        private async Task LoadAuthorsAsync(string searchTerm = null, string orderBy = null, int? skip = null, int? top = null)
        {
            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
            var query = $"{_configuration["ApiBaseUrl"]}/odata/Authors?$count=true";

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query += $"&$filter=contains(FirstName, '{searchTerm}') or contains(LastName, '{searchTerm}') or contains(City, '{searchTerm}')";
            }

            if (!string.IsNullOrEmpty(orderBy))
            {
                query += $"&$orderby={orderBy}";
            }

            if (skip.HasValue)
            {
                query += $"&$skip={skip.Value}";
            }

            if (top.HasValue)
            {
                query += $"&$top={top.Value}";
            }

            var response = await client.GetAsync(query);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var jObject = JObject.Parse(content);
                Authors = jObject["value"].ToObject<List<Author>>();
                TotalCount = jObject["@odata.count"].Value<int>();
            }
            else
            {
                Authors = new List<Author>();
                TotalCount = 0;
            }
        }

        public async Task<IActionResult> OnPostEditAsync([FromBody] Author author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
            var response = await client.PutAsJsonAsync($"{_configuration["ApiBaseUrl"]}/odata/Authors({author.AuthorId})", author);

            if (response.IsSuccessStatusCode)
            {
                return new OkResult();
            }
            else
            {
                return BadRequest("Error updating the author.");
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
            var response = await client.DeleteAsync($"{_configuration["ApiBaseUrl"]}/odata/Authors({id})");

            if (response.IsSuccessStatusCode)
            {
                return new OkResult();
            }
            else
            {
                return BadRequest("Error deleting the author.");
            }
        }
    }
}
