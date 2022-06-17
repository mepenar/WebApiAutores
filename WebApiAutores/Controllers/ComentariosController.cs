using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.DTOs;
using WebApiAutores.Entidades;

namespace WebApiAutores.Controllers

{
    [ApiController]
    [Route("api/libros/{libroId:int}/comentarios")]
    public class ComentariosController : ControllerBase
    {
        private readonly ApplicationDbConbext _context;
        private readonly IMapper _mapper;
        public ComentariosController(ApplicationDbConbext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ComentarioDTO>>> Get(int libroId)
        {
            var existeLibro = await _context.Libros.AnyAsync(x => x.Id == libroId);
            if (!existeLibro) return NotFound();

            var comentarios = await _context.Comentarios.Where(x => x.LibroId == libroId).ToListAsync();
            return _mapper.Map<List<ComentarioDTO>>(comentarios);
        }

        [HttpPost]
        public async Task<ActionResult> Post(int libroId, ComentarioCreacionDTO comentarioCreacionDto)
        {
            var existeLibro = await _context.Libros.AnyAsync(x => x.Id == libroId);
            if (!existeLibro) return NotFound();

            var comentario = _mapper.Map<Comentario>(comentarioCreacionDto);
            comentario.LibroId = libroId;
            _context.Add(comentario);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
