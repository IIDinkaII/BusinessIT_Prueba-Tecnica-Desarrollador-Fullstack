using System.ComponentModel.DataAnnotations;

namespace BusinessIT_Prueba_Tecnica_Desarrollador_Fullstack.DTO
{
    public class ServiceDTO
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string Description { get; set; } = string.Empty;
    }

    public class ReadServiceDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
