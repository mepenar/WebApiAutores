using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.Entidades;

namespace WebApiAutores.Controllers
{
    [ApiController]
    [Route("api/ppp")]
    public class PruebaController : ControllerBase
    {
        private readonly ApplicationDbConbext _context;

        public PruebaController(ApplicationDbConbext context)
        {
            this._context = context;
        }
        [HttpPost]
        public async Task<ActionResult> Post(Computador pc)
        {
            _context.Computadores.Add(pc);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("{marca:int}")]
        public async Task<ActionResult<Computador>> Get([FromRoute] int marca)
        {
            var computador = await _context.Computadores.Where(x => x.Id == marca).Include(x => x.Componentes).ThenInclude(y => y.Observacion).ToListAsync();
            return Ok(computador);
        }
    }
}
