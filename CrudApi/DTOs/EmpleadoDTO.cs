namespace CrudApi.DTOs
{
    public class EmpleadoDTO
    {
        public int IdEmpleado { get; set; }
        public String? Nombre { get; set; }
        public int Sueldo { get; set; }

        public int IdPerfil { get; set; }

        public string? NombrePerfil {  get; set; }

    }
}
