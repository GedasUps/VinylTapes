using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pgvector.EntityFrameworkCore;
using VinylTapes.Data;
using VinylTapes.Services;

namespace VinylTapes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VectorController : ControllerBase
    {
        private readonly VinylDbContext _context;
        private readonly VectorService _vectorService;


        public VectorController( VinylDbContext context, VectorService vectorService)
        {

            _context = context;
            _vectorService = vectorService;
        }
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query)) return BadRequest();

            // 1. Paverčiame paieškos frazę į vektorių
            var queryVector = _vectorService.GenerateVector(query);

            // 2. Vykdome vektorinę paiešką duomenų bazėje
            var results = await _context.Ploksteles
                .OrderBy(p => p.Vektorius!.CosineDistance(queryVector)) // Surandame mažiausią atstumą
                .Take(5)
                .ToListAsync();

            return Ok(results);
        }
    }
}
