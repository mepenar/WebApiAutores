using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.Controllers.Entidades;
using WebApiAutores.Servicios;

namespace WebApiAutores.Controllers
{
    [ApiController]
    [Route("api/autores")]
    public class AutoresController : ControllerBase
    {
        private readonly ApplicationDbConbext _context;
        private readonly IServicio _servicio;
        private readonly ServicioSingleton _servicioSingleton;
        private readonly ServicioScoped _servicioScoped;
        private readonly ServicioTransient _servicioTransient;
        private readonly ILogger<AutoresController> logger;

        public AutoresController(ApplicationDbConbext context, IServicio servicio, ServicioSingleton servicioSingleton,
            ServicioScoped servicioScoped, ServicioTransient servicioTransient, ILogger<AutoresController> logger)
        {
            this._context = context;
            _servicio = servicio;
            this._servicioSingleton = servicioSingleton;
            this._servicioScoped = servicioScoped;
            this._servicioTransient = servicioTransient;
            this.logger = logger;
        }

        [HttpGet("GUID")]
        public ActionResult ObtenerGuids()
        {
            return Ok(new
            {
                AutoresControllerTransient = _servicioTransient.Guid,
                ServicioA_Transient = _servicio.ObtenerTransient(),
                AutoresControllerSingleton = _servicioSingleton.Guid,
                ServicioA_Singleton = _servicio.ObtenerSingleton(),
                AutoresControllerScoped = _servicioScoped.Guid,
                ServicioA_Scoped = _servicio.ObtenerScoped(),
            });
        }

        [HttpGet]
        public async Task<ActionResult<List<Autor>>> Get()
        {
            logger.LogInformation("Estamos obteniendo los autores");
            logger.LogWarning("Mensaje de prueba");
            _servicio.RealizarTarea();
            return await _context.Autores.Include(x => x.Libros).ToListAsync();
        }

        [HttpGet("primero")]
        public async Task<ActionResult<Autor>> PrimerAutor([FromHeader] int myValor, [FromQuery] string nombre)
        {
            return await _context.Autores.FirstOrDefaultAsync();
        }

        [HttpGet("{id:int}/{param2}")]
        public async Task<ActionResult<Autor>> ById(int id, string param2)
        {
            var autor = await _context.Autores.FirstOrDefaultAsync(x => x.Id == id);
            if (autor == null) return NotFound();
            return autor;
        }

        [HttpGet("{nombre}")]
        public async Task<ActionResult<Autor>> Get(string nombre)
        {
            var autor = await _context.Autores.FirstOrDefaultAsync(x => x.Nombre.Contains(nombre));
            if (autor == null) return NotFound();
            return autor;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Autor autor)
        {
            _context.Add(autor);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Autor autor, int id)
        {
            if (autor.Id != id) return BadRequest("El id no coincide");

            var existe = await _context.Autores.AnyAsync(x => x.Id == id);
            if (!existe) return NotFound();

            _context.Update(autor);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await _context.Autores.AnyAsync(x => x.Id == id);
            if (!existe) return NotFound();

            _context.Remove(new Autor() { Id = id });
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
