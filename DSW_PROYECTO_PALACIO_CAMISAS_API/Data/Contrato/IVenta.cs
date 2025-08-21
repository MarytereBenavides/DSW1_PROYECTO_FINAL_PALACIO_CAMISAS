using DSW_PROYECTO_PALACIO_CAMISAS_API.Models;

namespace DSW_PROYECTO_PALACIO_CAMISAS_API.Data.Contrato
{
    public interface IVenta
    {
        List<Venta> Listado();
        Venta ObtenerPorID(int id);
        Venta Registrar(Venta venta, List<DetalleVenta> detalles);
    }
}
