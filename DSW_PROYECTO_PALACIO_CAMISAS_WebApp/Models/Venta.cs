using DSW_PROYECTO_PALACIO_CAMISAS_WebApp.ViewModels;

namespace DSW_PROYECTO_PALACIO_CAMISAS_WebApp.Models
{
    public class Venta
    {
        public int Id_Venta { get; set; }             // lo asigna el back
        public string Nombre_Cliente { get; set; } = "";
        public string Dni_Cliente { get; set; } = "";
        public string Tipo_Pago { get; set; } = "";
        public DateTime Fecha { get; set; }           // la pone el back
        public decimal Precio_Total { get; set; }     // back valida/calcula
        public string Estado { get; set; } = "Activo";
        public List<DetalleVenta> Detalles { get; set; } = new();
        public List<DetalleVentaTotal> DetallesTotal { get; set; } = new();
    }
}
