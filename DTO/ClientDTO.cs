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

}
