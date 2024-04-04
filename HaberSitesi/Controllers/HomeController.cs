using HaberSitesi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace HaberSitesi.Controllers
{
    public class HomeController : Controller
    {
        HttpResponseMessage getData = null;
        HttpClient client = new HttpClient();
        Uri baseAdress = new Uri("https://localhost:44373/api/Haber/");

        public HomeController()
        {
            client.BaseAddress = baseAdress;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // GET: api/Haber/GetAllHaber ---------------------------------------------
        public async Task<IActionResult> Index(string? kategori)
        {
            if (kategori != null)
            {
                getData = await client.PostAsJsonAsync<string>("GetAllHaber", kategori);
            }
            IEnumerable<Haber> habers;
            getData = await client.GetAsync("GetAllHaber");
            if (getData.IsSuccessStatusCode)
            {
                string results = getData.Content.ReadAsStringAsync().Result;
                habers = JsonConvert.DeserializeObject<List<Haber>>(results);
            }
            else
            {
                habers = Enumerable.Empty<Haber>();
                ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
            }
            
            return View(habers);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}