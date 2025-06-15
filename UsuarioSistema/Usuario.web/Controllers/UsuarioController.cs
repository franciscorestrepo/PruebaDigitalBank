using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using Usuario.Web.Models;

namespace Usuario.Web.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly HttpClient _httpClient;

        public UsuarioController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Api");
        }

       //  GET: /Usuario/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Usuario/Create        
        [HttpPost]
        public async Task<IActionResult> Create(UsuarioViewModel usuario)
        {
            Console.WriteLine("[POST Create] Iniciado");

            if (!ModelState.IsValid)
            {
                Console.WriteLine("[POST Create] ModelState inválido. Detalles de errores:");

                foreach (var key in ModelState.Keys)
                {
                    var state = ModelState[key];
                    foreach (var error in state.Errors)
                    {
                        Console.WriteLine($"[ModelState Error] Campo: {key}, Error: {error.ErrorMessage}");
                    }
                }

                return View(usuario);
            }

            var json = JsonSerializer.Serialize(usuario);
            Console.WriteLine("[POST Create] Payload JSON: " + json);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/usuarios", content);
            Console.WriteLine($"[POST Create] Status code: {response.StatusCode}");

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("[POST Create] Redirigiendo a Gestion");
                return RedirectToAction("Gestion");
            }

            var body = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"[POST Create] ERROR: {body}");

            ModelState.AddModelError("", "Error al guardar el usuario");
            return View(usuario);
        }


        public async Task<IActionResult> Gestion()
        {
            var response = await _httpClient.GetAsync("api/usuarios");
            var json = await response.Content.ReadAsStringAsync();
            var usuarios = JsonSerializer.Deserialize<List<UsuarioViewModel>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return View(usuarios);
        }

        
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"api/usuarios/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var usuario = JsonSerializer.Deserialize<UsuarioViewModel>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return View(usuario);
        }

        
        [HttpPost]
        public async Task<IActionResult> Edit(int id, UsuarioViewModel usuario)
        {
            if (!ModelState.IsValid) return View(usuario);

            var json = JsonSerializer.Serialize(usuario);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/usuarios/{id}", content);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Gestion");

            ModelState.AddModelError("", "Error al actualizar el usuario");
            return View(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/usuarios/{id}");
            return RedirectToAction("Gestion");
        }
    }
}
