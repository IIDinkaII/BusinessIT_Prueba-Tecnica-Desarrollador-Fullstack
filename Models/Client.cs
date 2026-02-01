using System.ComponentModel.DataAnnotations;

namespace BusinessIT_Prueba_Tecnica_Desarrollador_Fullstack.Models
{
    public class Client : BaseEntity
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public required string Name { get; set; }

        [Required]
        [MaxLength(255)]
        public required string Email { get; set; }
        
        public ICollection<ClientService> ClientServices { get; set; } = new List<ClientService>();
    }
}
