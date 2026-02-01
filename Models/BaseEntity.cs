namespace BusinessIT_Prueba_Tecnica_Desarrollador_Fullstack.Models
{
    public abstract class BaseEntity
    {
        public bool IsActive { get; set; } = true;

        public DateTime CreateDatetime { get; set; }

        public DateTime UpdateDatetime { get; set; }
    }
}
