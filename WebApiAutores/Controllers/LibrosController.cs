using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.DTOs;
using WebApiAutores.Entidades;

namespace WebApiAutores.Controllers
{
    [ApiController]
    [Route("api/libros")]
    public class LibrosController : ControllerBase
    {
        private readonly ApplicationDbConbext _context;
        private readonly IMapper _mapper;

        public LibrosController(ApplicationDbConbext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<LibroDTO>> Get(int id)
        {
            var libros = await _context.Libros.Include(x => x.Comenatrios).FirstOrDefaultAsync(x => x.Id == id);
            return _mapper.Map<LibroDTO>(libros);
        }

        [HttpPost]
        public async Task<ActionResult> Post(LibroCreacionDTO libroCreacionDTO)
        {
            if (libroCreacionDTO.AutoresIds == null) return BadRequest("No se puede crear libro sin autores");
            var autoresIds = await _context.Autores
                .Where(autorDB => libroCreacionDTO.AutoresIds.Contains(autorDB.Id))
                .Select(x => x.Id).ToListAsync();

            if (libroCreacionDTO.AutoresIds.Count != autoresIds.Count) return BadRequest("No existe uno de los autores");

            var libro = _mapper.Map<Libro>(libroCreacionDTO);
            _context.Add(libro);
            await _context.SaveChangesAsync();
            return Ok();

        }
    }
}
