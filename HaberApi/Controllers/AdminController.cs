using HaberApi.Data;
using HaberApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HaberApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ApiDbContext _db;

        public AdminController(ApiDbContext db)
        {
            _db = db;
        }

        // GET: api/Admin/GetAllAdmin
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Admin>>> GetAllAdmin()
        {
            if (_db.Adminler == null)
            {
                return NotFound();
            }
            return await _db.Adminler.ToListAsync();
        }
    }
}
