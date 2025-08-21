namespace DSW_PROYECTO_PALACIO_CAMISAS_WebApp.Models
{
    public class Camisa
    {
        public int Id_Camisa { get; set; }
        public string Descripcion { get; set; } = "";
        public int Id_Marca { get; set; }
        public string Color { get; set; } = "";
        public string Talla { get; set; } = "";
        public string Manga { get; set; } = "";
        public int Stock { get; set; }
        public decimal Precio_Costo { get; set; }
        public decimal Precio_Venta { get; set; }
        public int Id_Estante { get; set; }
        public string Estado { get; set; } = "";
        // opcional para mostrar en tablas
        public string? Marca { get; set; }
        public string? Estante { get; set; }
    }
}
