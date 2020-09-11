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
    public class EventoController:ControllerBase
    {
        private readonly ReservacionDataContext _baseDatos;
        public EventoController(ReservacionDataContext _context)
        {
            _baseDatos = _context;

            if (_baseDatos.eventos.Count() == 0)
            {
                _baseDatos.eventos.Add(new Evento {nombre_evento="Feria juniana ",lugar="Expocentro",precio=250,categoriaEventoid=1,invitadoid=1,clave="asd213"});
                _baseDatos.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<Evento>>> GetEventos()
        {
            return await _baseDatos.eventos.Include(q => q.invitado).Include(q => q.categoriaEvento).ToListAsync();

  
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Evento>> GetEvento(int id)
        {
            var eventos = await _baseDatos.eventos.Include(q => q.invitado).Include(q => q.categoriaEvento).FirstOrDefaultAsync(q => q.id == id);

            if (eventos == null)
            {
                return NotFound();
            }

            return eventos;
        }

        [HttpPost]
        public async Task<ActionResult<Evento>> PostEvento(Evento item)
        {
            _baseDatos.eventos.Add(item);
            await _baseDatos.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEvento), new { id = item.id }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvento(int id, Evento item)
        {
            if (id != item.id)
            {
                return BadRequest();
            }
            CategoriaEvento categoriaEvento = await _baseDatos.categoriaEventos.FirstOrDefaultAsync(q => q.id == item.categoriaEventoid);
            if (categoriaEvento == null)
            {
                return NotFound("La categoria no existe.");
            }
            
            _baseDatos.Entry(item).State = EntityState.Modified;
            await _baseDatos.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletEvento(int id)
        {
            var eventos = await _baseDatos.eventos.FindAsync(id);

            if (eventos == null)
            {
                return NotFound();
            }

            _baseDatos.eventos.Remove(eventos);
            await _baseDatos.SaveChangesAsync();

            return NoContent();
        }
    }
}
