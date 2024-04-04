using HaberSitesi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace HaberSitesi.Controllers
{
    public class HaberController : Controller
    {
        HttpResponseMessage getData = null;
        HttpClient client = new HttpClient();
        Uri baseAdress = new Uri("https://localhost:44373/api/Haber/");
        public HaberController()
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
            IEnumerable<Haber> habers = new List<Haber>();
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
        // ------------------------------------------------------------------------

        // POST: api/Haber/PostHaber ----------------------------------------------
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Haber haber)
        {
            if (haber == null)
                return BadRequest("Model boş.");
            // Api'ye yükle
            
            getData = await client.PostAsJsonAsync<Haber>("PostHaber", haber);
            if (getData.IsSuccessStatusCode)
            {
                var result = await getData.Content.ReadAsStringAsync();
                // response içeriğiyle ilgili işlem yapabilirsiniz.
                return RedirectToAction("Index");
            }
            else
            {
                return BadRequest("İşlem gerçekleştirilemedi");
            }
        }
        // ------------------------------------------------------------------------

        // PUT: api/Haber/PutHaber/id ---------------------------------------------
        public IActionResult Edit(int? id)
        {
            Haber haber = null;
            getData = client.GetAsync("GetHaber/" + id).Result;
            if (getData.IsSuccessStatusCode)
            {
                string result = getData.Content.ReadAsStringAsync().Result;
                haber = JsonConvert.DeserializeObject<Haber>(result);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                return BadRequest();
            }
            return View(haber);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Haber haber)
        {
            getData = await client.PutAsJsonAsync<Haber>("PutHaber/" +haber.HaberId.ToString(), haber);
            if (getData.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                Console.WriteLine("Error calling WEB Api!");
                return BadRequest();
            }
        }
        // ------------------------------------------------------------------------

        // DELETE: api/Haber/DeleteHaber/id ---------------------------------------
        public IActionResult Delete(int? id)
        {
            Haber haber = null;
            getData = client.GetAsync("GetHaber/" + id).Result;
            if (getData.IsSuccessStatusCode)
            {
                string result = getData.Content.ReadAsStringAsync().Result;
                haber = JsonConvert.DeserializeObject<Haber>(result);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                return BadRequest();
            }
            return View(haber);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            getData = await client.DeleteAsync("DeleteHaber/" + id.ToString());
            if (getData.IsSuccessStatusCode) 
            { 
                return RedirectToAction("Index");
            }
            else
            {
                Console.WriteLine("Error calling WEB Api!");
                return BadRequest();
            }
        }
        // ------------------------------------------------------------------------

        public IActionResult Details(int? id)
        {
            Haber haber = null;
            getData = client.GetAsync("GetHaber/" + id).Result;
            if (getData.IsSuccessStatusCode)
            {
                string result = getData.Content.ReadAsStringAsync().Result;
                haber = JsonConvert.DeserializeObject<Haber>(result);
                return View(haber);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                return BadRequest();
            }
        }
    }
}
