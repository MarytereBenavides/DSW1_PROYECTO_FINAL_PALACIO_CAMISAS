namespace DSW_PROYECTO_PALACIO_CAMISAS_API.Models.DTOs
{
    public class VentaDto
    {
        public int id_venta { get; set; }
        public string nombre_cliente { get; set; }
        public string dni_cliente { get; set; }
        public string tipo_pago { get; set; }
        public DateTime fecha { get; set; }
        public decimal precio_total { get; set; }
        public string estado { get; set; }
        public List<DetalleVentaDto>? detalles { get; set; }
    }

    public class DetalleVentaDto
    {
        public int id_camisa { get; set; }
        public string camisa_descripcion { get; set; }
        public string camisa_color { get; set; }
        public string camisa_talla { get; set; }
        public int cantidad { get; set; }
        public decimal precio { get; set; }
    }
}
