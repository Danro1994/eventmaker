using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EventMaker.DataContext;
using EventMaker.Modelos;

namespace EventMaker.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class CategoriaEventoController:ControllerBase
    {
        private readonly ReservacionDataContext _baseDatos;
        public CategoriaEventoController(ReservacionDataContext _context)
        {
            _baseDatos = _context;

            if (_baseDatos.categoriaEventos.Count() == 0)
            {
                _baseDatos.categoriaEventos.Add(new CategoriaEvento {categoria_evento="Concierto",icono="Grande"});
                _baseDatos.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaEvento>>> GetCategoriaEventos()
        {
            return await _baseDatos.categoriaEventos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaEvento>> GetCategoriaEvento(int id)
        {
            var CategoriaEvento = await _baseDatos.categoriaEventos.FirstOrDefaultAsync(q => q.id == id);

            if (CategoriaEvento == null)
            {
                return NotFound();
            }

            return CategoriaEvento;
        }

        [HttpPost]
        public async Task<ActionResult<CategoriaEvento>> PostCategoriaEvento(CategoriaEvento item)
        {
            _baseDatos.categoriaEventos.Add(item);
            await _baseDatos.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCategoriaEvento), new { id = item.id }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoriaEvento(int id, CategoriaEvento item)
        {
            if (id != item.id)
            {
                return BadRequest();
            }

            _baseDatos.Entry(item).State = EntityState.Modified;
            await _baseDatos.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletCategoriaEvento(int id)
        {
            var categoriaEventos = await _baseDatos.categoriaEventos.FindAsync(id);

            if (categoriaEventos == null)
            {
                return NotFound();
            }

            _baseDatos.categoriaEventos.Remove(categoriaEventos);
            await _baseDatos.SaveChangesAsync();

            return NoContent();
        }
    }
}
