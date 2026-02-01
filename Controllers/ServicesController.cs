using BusinessIT_Prueba_Tecnica_Desarrollador_Fullstack.Context;
using BusinessIT_Prueba_Tecnica_Desarrollador_Fullstack.DTO;
using BusinessIT_Prueba_Tecnica_Desarrollador_Fullstack.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BusinessIT_Prueba_Tecnica_Desarrollador_Fullstack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ServicesController(AppDbContext context)
        {
            _context = context;
        }

        // GET api/services
        [HttpGet]
        public async Task<IActionResult> GetAllServices()
        {
            var services = await _context.Services.Select(s => new
            {
                s.Id,
                s.Name,
                s.Description
            }).ToListAsync();
            return Ok(services);
        }

        // GET api/services/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetService(int id)
        {
            var service = await _context.Services
                .Where(s => s.Id == id)
                .Select(s => new
            {
                s.Id,
                s.Name,
                s.Description,
                s.CreateDatetime,
                s.UpdateDatetime
            }).FirstOrDefaultAsync();

            if (service == null)
            {
                return NotFound();
            }

            return Ok(service);
        }

        // POST api/services
        [HttpPost]
        public async Task<ActionResult<Service>> PostService(ServiceDTO service)
        {
            if (service == null)
            {
                return BadRequest();
            }

            var createService = new Service
            {
                Name = service.Name,
                Description = service.Description
            };

            _context.Services.Add(createService);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetService), new { id = createService.Id }, service);
        }

        // PUT api/services/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutService(int id, ServiceDTO service)
        {
            if (service == null )
            {
                return BadRequest();
            }

            var dbService = await _context.Services.FindAsync(id);

            if(dbService == null)
            {
                return NotFound();
            }

            // Update Date and State
            dbService.Name = service.Name;
            dbService.Description = service.Description;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Delete api/services
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            var service = await _context.Services.FindAsync(id);

            if (service == null)
            {
                return NotFound();
            }

            // Update Date and State
            _context.Services.Remove(service);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
