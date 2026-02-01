using BusinessIT_Prueba_Tecnica_Desarrollador_Fullstack.Context;
using BusinessIT_Prueba_Tecnica_Desarrollador_Fullstack.DTO;
using BusinessIT_Prueba_Tecnica_Desarrollador_Fullstack.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BusinessIT_Prueba_Tecnica_Desarrollador_Fullstack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClientsController(AppDbContext context)
        {
            _context = context;
        }

        // GET api/clients
        [HttpGet]
        public async Task<IActionResult> GetAllClients()
        {
            var clients = await _context.Clients.Select(c => new 
            {
                c.Id,
                c.Name,
                c.Email
            }).ToListAsync();
            return Ok(clients);
        }

        // GET api/clients/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClient(int id)
        {
            var client = await _context.Clients
                .Where(c => c.Id == id)
                .Select(c => new {
                    c.Id,
                    c.Name,
                    c.Email,
                    c.CreateDatetime,
                    c.UpdateDatetime,
                    Services = c.ClientServices.Select(cs => new { cs.Service.Id, cs.Service.Name, cs.Service.Description })
                })
                .FirstOrDefaultAsync();

            if (client == null)
            {
                return NotFound();
            }
            
            return Ok(client);
        }

        // POST api/clients
        [HttpPost]
        public async Task<ActionResult<Client>> PostClient(CreateUpdateClientDTO client)
        {
            if (client == null) 
            {
                return BadRequest();
            }

            var createClient = new Client
            {
                Name = client.Name,
                Email = client.Email
            };

            _context.Clients.Add(createClient);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetClient), new { id = createClient.Id } , client);
        }        
        
        // PUT api/client/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClient(int id, CreateUpdateClientDTO client)
        {
            if(client == null)
            {
                return BadRequest();
            }

            var dbClient = await _context.Clients.FindAsync(id);

            if(dbClient == null)
            {
                return NotFound();
            }

            // Update Date and State
            dbClient.Name = client.Name;
            dbClient.Email = client.Email;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var client = await _context.Clients.FindAsync(id);

            if(client == null)
            {
                return NotFound();
            }

            // Update Date and State
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // RELATION CLIENT - SERVICES 

        [HttpPost("{clientId}/services/{serviceId}")]
        public async Task<IActionResult> AddServiceToClient(int clientId, int serviceId)
        {
            // Check Client and Service
            var client = await _context.Clients.FindAsync(clientId);
            var service = await _context.Services.FindAsync(serviceId);

            if (client == null || service == null)
            {
                return NotFound("Cliente o Servicio no encontrado");
            }
                
            // Check Exist Relation
            var exists = await _context.ClientServices
                .AnyAsync(cs => cs.ClientId == clientId && cs.ServiceId == serviceId);

            if (exists)
            {
                return BadRequest("El servicio ya está asignado al cliente");
            }

            // Create relation
            var clientService = new ClientService
            {
                ClientId = clientId,
                ServiceId = serviceId
            };

            _context.ClientServices.Add(clientService);
            await _context.SaveChangesAsync();

            return Ok("Servicio agregado al cliente correctamente");
        }

        // DELETE api/clients/{clientId}/services/{serviceId}
        [HttpDelete("{clientId}/services/{serviceId}")]
        public async Task<IActionResult> RemoveServiceFromClient(int clientId, int serviceId)
        {
            // Check Exist Relation
            var clientService = await _context.ClientServices
                .FirstOrDefaultAsync(cs => cs.ClientId == clientId && cs.ServiceId == serviceId);

            if (clientService == null)
            {
                return NotFound();
            }
                

            _context.ClientServices.Remove(clientService);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
