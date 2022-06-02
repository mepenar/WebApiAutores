﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.Controllers.Entidades;

namespace WebApiAutores.Controllers
{
    [ApiController]
    [Route("api/libros")]
    public class LibrosController : ControllerBase
    {
        private readonly ApplicationDbConbext _context;
        public LibrosController(ApplicationDbConbext context)
        {
            this._context = context;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Libro>> Get(int id)
        {
            return await _context.Libros.Include(x => x.Autor).FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Libro libro)
        {
            var existeAutor = await _context.Autores.AnyAsync(x => x.Id == libro.AutorId);
            if (!existeAutor) return BadRequest($"No existe el autor de Id:{libro.AutorId}");

            _context.Add(libro);
            await _context.SaveChangesAsync();
            return Ok();

        }
    }
}
