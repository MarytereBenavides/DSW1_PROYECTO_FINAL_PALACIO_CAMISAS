namespace DSW_PROYECTO_PALACIO_CAMISAS_API.Models.DTOs
{
    public class VentaCreateDto
    {
        public string nombre_cliente { get; set; }
        public string dni_cliente { get; set; }
        public string tipo_pago { get; set; }
        public List<DetalleVentaCreateDto> detalles { get; set; }
    }
    public class DetalleVentaCreateDto
    {
        public int id_camisa { get; set; }
        public int cantidad { get; set; }
        public decimal precio { get; set; }
    }
}
