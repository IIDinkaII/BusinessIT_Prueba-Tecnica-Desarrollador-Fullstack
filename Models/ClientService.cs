using System.Text.Json.Serialization;

namespace BusinessIT_Prueba_Tecnica_Desarrollador_Fullstack.Models
{
    public class ClientService
    {
        public int ClientId {  get; set; }
        public Client Client { get; set; } = null!;

        public int ServiceId { get; set; }
        public Service Service { get; set; } = null!;

    }
}
