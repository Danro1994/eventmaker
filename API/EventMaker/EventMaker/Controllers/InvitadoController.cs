using EventMaker.DataContext;
using EventMaker.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace EventMaker.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class InvitadoController:ControllerBase
    {
        private readonly ReservacionDataContext _baseDatos;
        public InvitadoController(ReservacionDataContext _context)
        {
            _baseDatos = _context;

            if (_baseDatos.invitados.Count() == 0)
            {
                _baseDatos.invitados.Add(new Invitado {nombre_invitado="Kazzabe" ,apellido_invitado="Agrupacion",descripcion="Grupo Hondureño" });
                _baseDatos.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Invitado>>> Getinvitados()
        {
            return await _baseDatos.invitados.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Invitado>> Getinvitados(int id)
        {
            var invitados = await _baseDatos.invitados.FirstOrDefaultAsync(q => q.id == id);

            if (invitados == null)
            {
                return NotFound();
            }

            return invitados;
        }

        [HttpPost]
        public async Task<ActionResult<Invitado>> Postinvitados(Invitado item)
        {
            _baseDatos.invitados.Add(item);
            await _baseDatos.SaveChangesAsync();

            return CreatedAtAction(nameof(Getinvitados), new { id = item.id }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Putinvitados(int id, Invitado item)
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
        public async Task<IActionResult> Deletinvitados(int id)
        {
            var invitados = await _baseDatos.invitados.FindAsync(id);

            if (invitados == null)
            {
                return NotFound();
            }

            _baseDatos.invitados.Remove(invitados);
            await _baseDatos.SaveChangesAsync();

            return NoContent();
        }
    }
}
