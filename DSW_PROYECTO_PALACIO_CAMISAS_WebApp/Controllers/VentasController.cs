using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using DSW_PROYECTO_PALACIO_CAMISAS_WebApp.Models;
using DSW_PROYECTO_PALACIO_CAMISAS_WebApp.ViewModels;

namespace DSW_PROYECTO_PALACIO_CAMISAS_WebApp.Controllers
{
    public class VentasController : Controller
    {
        private readonly IHttpClientFactory _http;
        private HttpClient Api => _http.CreateClient("Api");
        public VentasController(IHttpClientFactory http) => _http = http;

        // ✅ GET: /Ventas/Index (lista con detalles, usando mock si no hay API)
        [HttpGet]
        public IActionResult Index()
        {
            // Datos simulados
            var ventas = new List<Venta>
            {
                new Venta
                {
                    id_venta = 1,
                    nombre_cliente = "Juan Pérez",
                    dni_cliente = "12345678",
                    tipo_pago = "Efectivo",
                    fecha = DateTime.Now,
                    detalles = new List<DetalleVenta>
                    {
                        new DetalleVenta
                        {
                            Id_Camisa = 1,
                            Cantidad = 2,
                            Precio = 50,
                        },
                        new DetalleVenta
                        {
                            Id_Camisa = 2,
                            Cantidad = 1,
                            Precio = 70,
                        }
                    }
                },
                new Venta
                {
                    id_venta = 2,
                    nombre_cliente = "María Gómez",
                    dni_cliente = "87654321",
                    tipo_pago = "Tarjeta",
                    fecha = DateTime.Now.AddDays(-1),
                    detalles = new List<DetalleVenta>
                    {
                        new DetalleVenta
                        {
                            Id_Camisa = 3,
                            Cantidad = 3,
                            Precio = 45
                        }
                    }
                }
            };

            return View(ventas);
        }

        // GET: /Ventas/Create
        [HttpGet]
        public IActionResult Create()
        {
            var vm = new VentaCreate();
            return View(vm);
        }

        // POST: /Ventas/AgregarLinea (desde el form)
        [HttpPost]
        public async Task<IActionResult> AgregarVenta(VentaCreate vm)
        {
            if (vm.CamisaSeleccionadaId is null) return View("Create", vm);
            var camisa = await Api.GetFromJsonAsync<Camisa>($"camisas/{vm.CamisaSeleccionadaId}");
            if (camisa == null) { ModelState.AddModelError("", "Camisa no encontrada"); return View("Create", vm); }

            vm.Lineas.Add(new DetalleVentaTotal
            {
                Id_Camisa = camisa.id_camisa,
                Descripcion = camisa.descripcion,
                Presentacion = $"{camisa.color} / {camisa.talla} / {camisa.manga}",
                Cantidad = vm.Cantidad,
                PrecioUnitario = vm.PrecioUnitario > 0 ? vm.PrecioUnitario : camisa.precio_venta
            });
            // limpia selección
            vm.CamisaSeleccionadaId = null; vm.Cantidad = 1; vm.PrecioUnitario = 0;
            return View("Create", vm);
        }

        // POST: /Ventas/QuitarLinea
        [HttpPost]
        public IActionResult QuitarVenta(VentaCreate venta, int idx)
        {
            if (idx >= 0 && idx < venta.Lineas.Count) venta.Lineas.RemoveAt(idx);
            return View("Create", venta);
        }

        // GET: /Ventas/BuscarCamisas (usado por modal ajax)
        [HttpGet]
        public async Task<IActionResult> BuscarCamisas([FromQuery] CamisaFiltro filtro)
        {
            var q = $"camisas?marcaId={filtro.MarcaId}&tipo={filtro.Tipo}&talla={filtro.Talla}&manga={filtro.Manga}&color={filtro.Color}";
            var data = await Api.GetFromJsonAsync<List<Camisa>>(q) ?? new();
            return PartialView("_BuscarCamisasPartial", data);
        }

        // POST: /Ventas/Create  (registrar venta en el back)
        [HttpPost]
        public async Task<IActionResult> Create(VentaCreate venta, string? action)
        {
            if (action == "Agregar") return await AgregarVenta(venta);
            if (action?.StartsWith("Quitar:") == true)
            {
                var idx = int.Parse(action.Split(':')[1]);
                return QuitarVenta(venta, idx);
            }

            if (!venta.Lineas.Any())
            {
                ModelState.AddModelError("", "Agrega al menos una camisa.");
                return View(venta);
            }

            var dto = new Venta
            {
                nombre_cliente = venta.Nombre_Cliente,
                dni_cliente = venta.Dni_Cliente,
                tipo_pago = venta.Tipo_Pago,
                detalles = venta.Lineas.Select(l => new DetalleVenta
                {
                    Id_Camisa = l.Id_Camisa,
                    Cantidad = l.Cantidad,
                    Precio = l.PrecioUnitario,
                    Estado = "Activo"
                }).ToList()
            };

            var res = await Api.PostAsJsonAsync("ventas", dto);
            if (res.IsSuccessStatusCode)
            {
                TempData["msg"] = "Venta Registrada Satisfactoriamente. Por favor imprimir boleta.";
                return RedirectToAction(nameof(Comprobante));
            }

            ModelState.AddModelError("", "No se pudo registrar la venta.");
            return View(venta);
        }

        // GET: /Ventas/Comprobante  (placeholder para impresión)
        public IActionResult Comprobante() => View();
    }
}
