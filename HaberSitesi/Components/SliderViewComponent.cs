using HaberSitesi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace HaberSitesi.Components
{
    public class SliderViewComponent : ViewComponent
    {
        HttpResponseMessage getData = null;
        HttpClient client = new HttpClient();
        Uri baseAdress = new Uri("https://localhost:44373/api/Haber/");
        public SliderViewComponent() 
        {
            client.BaseAddress = baseAdress;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            IEnumerable<Haber> habers = new List<Haber>();
            getData = await client.GetAsync("GetAllHaber");
            if (getData.IsSuccessStatusCode)
            {
                string results = getData.Content.ReadAsStringAsync().Result;
                habers = JsonConvert.DeserializeObject<List<Haber>>(results).Take(3);
            }
            else
            {
                habers = Enumerable.Empty<Haber>();
                ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
            }
            return View(habers);
        }
    }
}
