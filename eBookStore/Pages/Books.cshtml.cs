using BusinessObject.Models;
using eBookStore.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace eBookStore.Pages
{
    public class BooksModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;

        public BooksModel(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
        }

        [BindProperty]
        public Book Book { get; set; }
        public List<Book> Books { get; set; }
        public List<Publisher> Publishers { get; set; }
        public string SearchTerm { get; set; }
        public string SearchField { get; set; }
        public string OrderBy { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalCount { get; set; }

        public async Task OnGetAsync(string searchTerm, string orderBy, int? pageNumber)
        {
            SearchTerm = searchTerm;
            OrderBy = orderBy;
            CurrentPage = pageNumber ?? 1;

            await LoadBooksAsync();
            await LoadPublishersAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadBooksAsync();
                await LoadPublishersAsync();
                return Page();
            }

            var client = _clientFactory.CreateClient();
            var response = await client.PostAsJsonAsync($"{_configuration["ApiBaseUrl"]}/odata/Books", Book);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error saving the book.");
                await LoadBooksAsync();
                await LoadPublishersAsync();
                return Page();
            }
        }

        private async Task LoadBooksAsync()
        {
            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
            var query = $"{_configuration["ApiBaseUrl"]}/odata/Books?$count=true";

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                query += $"&$filter=contains(Title, '{SearchTerm}') or contains(Type, '{SearchTerm}')";
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
                Books = jObject["value"].ToObject<List<Book>>();
                TotalCount = jObject["@odata.count"].Value<int>();
            }
            else
            {
                Books = new List<Book>();
                TotalCount = 0;
            }
        }

        private async Task LoadPublishersAsync()
        {
            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
            var response = await client.GetAsync($"{_configuration["ApiBaseUrl"]}/odata/Publishers");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var jObject = JObject.Parse(content);
                Publishers = jObject["value"].ToObject<List<Publisher>>();
            }
            else
            {
                Publishers = new List<Publisher>();
            }
        }

        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < (int)Math.Ceiling((double)TotalCount / PageSize);

        public async Task<IActionResult> OnPostEditAsync([FromBody] Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
            var response = await client.PutAsJsonAsync($"{_configuration["ApiBaseUrl"]}/odata/Books({book.BookId})", book);

            if (response.IsSuccessStatusCode)
            {
                return new OkResult();
            }
            else
            {
                return BadRequest("Error updating the book.");
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
            var response = await client.DeleteAsync($"{_configuration["ApiBaseUrl"]}/odata/Books({id})");

            if (response.IsSuccessStatusCode)
            {
                return new OkResult();
            }
            else
            {
                return BadRequest("Error deleting the book.");
            }
        }
    }
}