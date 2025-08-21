using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using DSW_PROYECTO_PALACIO_CAMISAS_WebApp.Models;
using DSW_PROYECTO_PALACIO_CAMISAS_WebApp.ViewModels;

namespace DSW_PROYECTO_PALACIO_CAMISAS_WebApp.Controllers
{
    public class CamisasController : Controller
    {
        private readonly IHttpClientFactory _http;
        public CamisasController(IHttpClientFactory http) => _http = http;

        private HttpClient Api => _http.CreateClient("Api");

        // GET: /Camisas
        public async Task<IActionResult> Index([FromQuery] CamisaFiltro filtro)
        {
            // combos
            var marcas = await Api.GetFromJsonAsync<List<Marca>>("marcas");
            ViewBag.Marcas = new SelectList(marcas ?? new(), "Id_Marca", "Descripcion");

            // query
            var q = $"camisas?marcaId={filtro.MarcaId}&tipo={filtro.Tipo}&talla={filtro.Talla}&manga={filtro.Manga}&color={filtro.Color}";
            var data = await Api.GetFromJsonAsync<List<Camisa>>(q) ?? new();
            return View(data);
        }

        // GET: /Camisas/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await CargarCombos();
            return View(new Camisa());
        }

        // POST: /Camisas/Create
        [HttpPost]
        public async Task<IActionResult> Create(Camisa camisa)
        {
            if (!ModelState.IsValid) { await CargarCombos(); return View(camisa); }
            var res = await Api.PostAsJsonAsync("camisas", camisa);
            if (res.IsSuccessStatusCode) return RedirectToAction(nameof(Index));
            ModelState.AddModelError("", "No se pudo crear la camisa");
            await CargarCombos();
            return View(camisa);
        }

        // GET: /Camisas/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            await CargarCombos();
            var camisa = await Api.GetFromJsonAsync<Camisa>($"camisas/{id}");
            if (camisa == null) return NotFound();
            return View(camisa);
        }

        // POST: /Camisas/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Camisa camisa)
        {
            if (id != camisa.Id_Camisa) return BadRequest();
            if (!ModelState.IsValid) { await CargarCombos(); return View(camisa); }
            var res = await Api.PutAsJsonAsync($"camisas/{id}", camisa);
            if (res.IsSuccessStatusCode) return RedirectToAction(nameof(Index));
            ModelState.AddModelError("", "No se pudo actualizar la camisa");
            await CargarCombos();
            return View(camisa);
        }

        // POST: /Camisas/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await Api.DeleteAsync($"camisas/{id}");
            TempData["msg"] = res.IsSuccessStatusCode ? "Eliminado" : "No se pudo eliminar";
            return RedirectToAction(nameof(Index));
        }

        // GET: /Camisas/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var camisa = await Api.GetFromJsonAsync<Camisa>($"camisas/{id}");
            return camisa is null ? NotFound() : View(camisa);
        }

        private async Task CargarCombos()
        {
            var marcas = await Api.GetFromJsonAsync<List<Marca>>("marcas") ?? new();
            ViewBag.Marcas = new SelectList(marcas, "Id_Marca", "Descripcion");
        }
    }
}
