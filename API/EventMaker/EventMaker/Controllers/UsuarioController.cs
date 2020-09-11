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
    public class UsuarioController:ControllerBase
    {
        private readonly ReservacionDataContext _baseDatos;
        public UsuarioController(ReservacionDataContext _context)
        {
            _baseDatos = _context;

            if (_baseDatos.usuarios.Count() == 0)
            {
                _baseDatos.usuarios.Add(new Usuario {nombre_usuario="Manuel",apellido_usuario="Madrid",edad=53,correo_electronico="jose.madrid@hondutel.hn"});
                _baseDatos.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            return await _baseDatos.usuarios.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuarios = await _baseDatos.usuarios.FirstOrDefaultAsync(q => q.id == id);

            if (usuarios == null)
            {
                return NotFound();
            }

            return usuarios;
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario item)
        {
            _baseDatos.usuarios.Add(item);
            await _baseDatos.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsuario), new { id = item.id }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario item)
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
        public async Task<IActionResult> Deletusuarios(int id)
        {
            var usuarios = await _baseDatos.usuarios.FindAsync(id);

            if (usuarios == null)
            {
                return NotFound();
            }

            _baseDatos.usuarios.Remove(usuarios);
            await _baseDatos.SaveChangesAsync();

            return NoContent();
        }
    }
}
