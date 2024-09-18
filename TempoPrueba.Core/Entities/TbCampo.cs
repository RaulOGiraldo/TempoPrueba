namespace InsttanttFlujos.Core.Entities
{
    public class TbCampo
    {
        public string IdCampo { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Tipo { get; set; } = null!;
        public int Longitud { get; set; }
        public int NroDecimales { get; set; }
        public string? Validacion { get; set; }
    }
}
