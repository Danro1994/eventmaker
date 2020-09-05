using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventMaker.DataContext;
using EventMaker.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventMaker.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ReservacionController:ControllerBase
    {
        private readonly ReservacionDataContext _baseDatos;
        public ReservacionController(ReservacionDataContext _context)
        {
            _baseDatos = _context;

            if (_baseDatos.reservacions.Count() == 0)
            {
                _baseDatos.reservacions.Add(new Reservacion {usuarioid=1});
                _baseDatos.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<Reservacion>>> Getreservacions()
        {
            List<Reservacion> reservacions = new List<Reservacion>();
            reservacions = await _baseDatos.reservacions.Include(q => q.usuario).Include(q => q.eventos).ToListAsync();
            foreach (var item in reservacions)
            {
                item.eventos.ForEach(q => q.categoriaEvento = _baseDatos.categoriaEventos.FirstOrDefault(r => r.id == q.categoriaEventoid));
                item.eventos.ForEach(q => q.invitado = _baseDatos.invitados.FirstOrDefault(r => r.id == q.invitadoid));
            }
            return reservacions;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Reservacion>> Getreservacions(int id)
        {
            var reservacions = await _baseDatos.reservacions.Include(q => q.usuario).Include(q => q.eventos).FirstOrDefaultAsync(q => q.id == id);

            if (reservacions == null)
            {
                return NotFound();
            }

            return reservacions;
        }

        [HttpPost]
        public async Task<ActionResult<Reservacion>> Postreservacions(Reservacion item)
        {
           
            
      
            Usuario usuario = await _baseDatos.usuarios.FirstOrDefaultAsync(q => q.id == item.usuarioid);
            if (usuario.edad < 21)
            {
                return NotFound("La reservacion debe ser por alguien mayor de 21 años");
            }     
          
            _baseDatos.reservacions.Add(item);
            await _baseDatos.SaveChangesAsync();

            return CreatedAtAction(nameof(Getreservacions), new { id = item.id }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Putreservacions(int id, Reservacion item)
        {
            if (id != item.id)
            {
                return BadRequest();
            }

            
            Usuario usuario = await _baseDatos.usuarios.FirstOrDefaultAsync(q => q.id == item.usuarioid);
            if (usuario == null)
            {
                return NotFound("El Registro no existe.");
            }
            if (usuario.edad <21 )
            {
                return NotFound("La reservacion debe ser por alguien mayor de 18 años");
            }

          
            _baseDatos.Entry(item).State = EntityState.Modified;
            await _baseDatos.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletreservacions(int id)
        {
            var reservacions = await _baseDatos.reservacions.FindAsync(id);

            if (reservacions == null)
            {
                return NotFound();
            }

            _baseDatos.reservacions.Remove(reservacions);
            await _baseDatos.SaveChangesAsync();

            return NoContent();
        }

    }
}
