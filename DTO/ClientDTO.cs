using System.ComponentModel.DataAnnotations;

namespace BusinessIT_Prueba_Tecnica_Desarrollador_Fullstack.DTO
{
    public class CreateUpdateClientDTO
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string Email { get; set; } = string.Empty;
    }

    public class ClientWithServicesDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDatetime { get; set; }
        public DateTime UpdateDatetime { get; set; }
        public List<ServiceDTO> Services { get; set; } = new List<ServiceDTO>();
    }
}
