namespace CrudApi.Entities
{
    public class Empleado
    {
        public int IdEmpleado  { get; set; }
        public String? Nombre { get; set; }
        public int Sueldo { get; set; }

        public int IdPerfil{ get; set; }

        public virtual Perfil PerfilReferencia { get; set; }
    }
}
