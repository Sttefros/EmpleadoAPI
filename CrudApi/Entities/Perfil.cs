using System.ComponentModel.DataAnnotations;

namespace CrudApi.Entities
{
    public class Perfil
    {
        [Key]
        public int IdPerfil { get; set; }

        public string? Nombre{ get; set; }

        public virtual ICollection<Empleado> EmpleadoReferencia { get; set; }
    }
}
