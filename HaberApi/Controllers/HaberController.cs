using HaberApi.Data;
using HaberApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net.Http.Headers;

namespace HaberApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HaberController : ControllerBase
    {
        private readonly ApiDbContext _db;

        public HaberController(ApiDbContext db)
        {
            _db = db;
        }

        // GET: api/Haber/GetAllHaber
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Haber>>> GetAllHaber(string? haber)
        {
            if (_db.Haberler == null)
            {
                return NotFound();
            }
            if (haber != null)
            {
                var data = _db.Haberler.Where(x => x.Kategori.Contains(haber)).OrderByDescending(x => x.HaberId);
                return await data.ToListAsync();
            } else
            {
                var list = _db.Haberler.OrderByDescending(x => x.HaberId);
                return await list.ToListAsync();
            }
        }

        // GET: api/Haber/GetHaber/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Haber>> GetHaber(int id)
        {
            if (_db.Haberler == null)
            {
                return NotFound();
            }
            var haber = await _db.Haberler.FindAsync(id);
            if (haber == null)
            {
                return NotFound();
            }
            return haber;
        }

        // POST: api/Haber/PostHaber
        [HttpPost]
        public async Task<ActionResult<Haber>> PostHaber(Haber haber)
        {
            haber.HaberGorsel = "test";
            _db.Haberler.Add(haber);
            await _db.SaveChangesAsync();
            return Ok(true);
        }

        // PUT: api/Haber/PutHaber/id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHaber(int id, Haber haber)
        {
            if (id != haber.HaberId)
            {
                return BadRequest();
            }
            _db.Entry(haber).State = EntityState.Modified;
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HaberExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        private bool HaberExists(long id)
        {
            return (_db.Haberler?.Any(e => e.HaberId == id)).GetValueOrDefault();
        }

        // DELETE: api/Haber/DeleteHaber/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHaber(int id)
        {
            if (_db.Haberler == null)
            {
                return NotFound();
            }
            var haber = await _db.Haberler.FindAsync(id);
            if (haber == null)
            {
                return NotFound();
            }
            _db.Haberler.Remove(haber);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
