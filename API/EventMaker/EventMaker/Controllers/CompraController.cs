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
    public class CompraController:ControllerBase
    {
        private readonly ReservacionDataContext _baseDatos;
        public CompraController(ReservacionDataContext _context)
        {
            _baseDatos = _context;

            if (_baseDatos.compras.Count() == 0)
            {
                _baseDatos.compras.Add(new Modelos.Compra { usuarioid = 1, eventoid=1});
                _baseDatos.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<Modelos.Compra>>> Getcompras()
        {
            var compras = await _baseDatos.compras.Include(q => q.usuario).Include(q => q.evento).ToListAsync();

            if (compras == null)
            {
                return NotFound();
            }

            return compras;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Modelos.Compra>> Getcompras(int id)
        {
            var compras = await _baseDatos.compras.Include(q => q.usuario).Include(q => q.evento).FirstOrDefaultAsync(q => q.id == id);

            if (compras == null)
            {
                return NotFound();
            }

            return compras;
        }

        [HttpPost]
        public async Task<ActionResult<Modelos.Compra>> Postcompras(Modelos.Compra item)
        {
           
            
      
            Usuario usuario = await _baseDatos.usuarios.FirstOrDefaultAsync(q => q.id == item.usuarioid);
            if (usuario.edad < 21)
            {
                return NotFound("La reservacion debe ser por alguien mayor de 21 años");
            }     
          
            _baseDatos.compras.Add(item);
            await _baseDatos.SaveChangesAsync();

            return CreatedAtAction(nameof(Getcompras), new { id = item.id }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Putcompras(int id, Modelos.Compra item)
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
        public async Task<IActionResult> Deletcompras(int id)
        {
            var compras = await _baseDatos.compras.FindAsync(id);

            if (compras == null)
            {
                return NotFound();
            }

            _baseDatos.compras.Remove(compras);
            await _baseDatos.SaveChangesAsync();

            return NoContent();
        }

    }
}
