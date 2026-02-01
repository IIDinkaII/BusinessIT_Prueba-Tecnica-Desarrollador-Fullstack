using System.ComponentModel.DataAnnotations;

namespace BusinessIT_Prueba_Tecnica_Desarrollador_Fullstack.Models
{
    public class Service: BaseEntity
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public required string Name { get; set; }

        [Required]
        [MaxLength(255)]
        public required string Description { get; set; }

        public ICollection<ClientService> ClientServices {  get; set; } = new List<ClientService>();
    }
}
