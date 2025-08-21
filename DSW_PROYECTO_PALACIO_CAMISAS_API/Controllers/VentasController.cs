using DSW_PROYECTO_PALACIO_CAMISAS_API.Data.Contrato;
using DSW_PROYECTO_PALACIO_CAMISAS_API.Models.DTOs;
using DSW_PROYECTO_PALACIO_CAMISAS_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DSW_PROYECTO_PALACIO_CAMISAS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        private readonly IVenta ventaDB;

        public VentasController(IVenta ventaRepo)
        {
            ventaDB = ventaRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var ventas = await Task.Run(() => ventaDB.Listado());
            var ventasDto = ventas.Select(v => new VentaDto
            {
                id_venta = v.id_venta,
                nombre_cliente = v.nombre_cliente,
                dni_cliente = v.dni_cliente,
                tipo_pago = v.tipo_pago,
                fecha = v.fecha,
                precio_total = v.precio_total,
                estado = v.estado
            }).ToList();

            return Ok(ventasDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var venta = await Task.Run(() => ventaDB.ObtenerPorID(id));
            if (venta == null)
                return NotFound();

            return Ok(venta);
        }

        [HttpPost]
        public async Task<IActionResult> Registrar([FromBody] VentaCreateDto ventaDto)
        {
            var venta = new Venta
            {
                nombre_cliente = ventaDto.nombre_cliente,
                dni_cliente = ventaDto.dni_cliente,
                tipo_pago = ventaDto.tipo_pago,
                fecha = DateTime.Now,
                precio_total = ventaDto.detalles.Sum(d => d.precio),
                estado = "Activo"
            };

            var detalles = ventaDto.detalles.Select(d => new DetalleVenta
            {
                id_camisa = d.id_camisa,
                cantidad = d.cantidad,
                precio = d.precio,
                estado = "Activo"
            }).ToList();

            var nuevaVenta = await Task.Run(() => ventaDB.Registrar(venta, detalles));
            return Ok(nuevaVenta);
        }
    }
}
