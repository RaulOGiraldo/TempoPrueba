namespace InsttanttFlujos.Core.Entities
{
    public partial class ExpAudit
    {
        public int Id { get; set; }
        public DateTime? Fecha { get; set; }
        public string? Usuario { get; set; }
        public string? Terminal { get; set; }
        public string? Accion { get; set; }
        public string? Tabla { get; set; }
        public string? Identificador { get; set; }
        public string? Aplicacion { get; set; }
        public string? Justificacion { get; set; }
    }
}
