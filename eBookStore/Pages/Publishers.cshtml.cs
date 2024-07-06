using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace eBookStore.Pages
{
    public class PublishersModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;

        public PublishersModel(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
        }

        [BindProperty]
        public Publisher Publisher { get; set; }
        public List<Publisher> Publishers { get; set; }
        public List<string> Cities { get; set; }
        public string SearchTerm { get; set; }
        public string OrderBy { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalCount { get; set; }

        public async Task OnGetAsync(string searchTerm, string orderBy, int? pageNumber)
        {
            SearchTerm = searchTerm;
            OrderBy = orderBy;
            CurrentPage = pageNumber ?? 1;

            await LoadPublishersAsync();
            await LoadCitiesAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadPublishersAsync();
                await LoadCitiesAsync();
                return Page();
            }

            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
            var response = await client.PostAsJsonAsync($"{_configuration["ApiBaseUrl"]}/odata/Publishers", Publisher);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error saving the publisher.");
                await LoadPublishersAsync();
                await LoadCitiesAsync();
                return Page();
            }
        }

        private async Task LoadPublishersAsync()
        {
            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
            var query = $"{_configuration["ApiBaseUrl"]}/odata/Publishers?$count=true";

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                query += $"&$filter=contains(PublisherName, '{SearchTerm}') or contains(City, '{SearchTerm}')";
            }

            if (!string.IsNullOrEmpty(OrderBy))
            {
                query += $"&$orderby={OrderBy}";
            }

            query += $"&$skip={(CurrentPage - 1) * PageSize}&$top={PageSize}";

            var response = await client.GetAsync(query);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var jObject = JObject.Parse(content);
                Publishers = jObject["value"].ToObject<List<Publisher>>();
                TotalCount = jObject["@odata.count"].Value<int>();
            }
            else
            {
                Publishers = new List<Publisher>();
                TotalCount = 0;
            }
        }

        private async Task LoadCitiesAsync()
        {
            Cities = new List<string> { "New York", "Los Angeles", "Chicago", "Houston", "Phoenix" };
        }

        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < (int)Math.Ceiling((double)TotalCount / PageSize);

        public async Task<IActionResult> OnPostEditAsync([FromBody] Publisher publisher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
            var response = await client.PutAsJsonAsync($"{_configuration["ApiBaseUrl"]}/odata/Publishers({publisher.PubId})", publisher);

            if (response.IsSuccessStatusCode)
            {
                return new OkResult();
            }
            else
            {
                return BadRequest("Error updating the publisher.");
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
            var response = await client.DeleteAsync($"{_configuration["ApiBaseUrl"]}/odata/Publishers({id})");

            if (response.IsSuccessStatusCode)
            {
                return new OkResult();
            }
            else
            {
                return BadRequest("Error deleting the publisher.");
            }
        }
    }
}
