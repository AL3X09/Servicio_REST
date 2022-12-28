using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Servicio_REST.Data;
using Servicio_REST.EFModels;

namespace Servicio_REST
{
    [Route("api/Ciudad")]
    [ApiController]
    public class CiudadControllerApi : ControllerBase
    {
        private readonly DB_Context _context;

        public CiudadControllerApi(DB_Context context)
        {
            _context = context;
        }

        /// <summary>
        /// API construida para hacer la prueba de los datos y que la herramienta no presente problemas
        /// </summary>
        /// <returns>Un Conjunto de datos de 10 datos</returns>
        /// <remarks>API de pruebas</remarks>
        /// <response code="200">La solicitud ha tenido éxito</response>
        /// <response code="400">Los datos de retorno son null</response>
        /// <response code="401">Desautorizado, no se ha proporcionado la llave de autenticación</response>
        /// <response code="500">Error en el servidor no responde, el servidor esta apagado o los servidores no retornan datos</response>
        // GET: api/CiudadControllerApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ciudad>>> GetCiudads()
        {
            return await _context.Ciudads.ToListAsync();
        }

        // GET: api/CiudadControllerApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ciudad>> GetCiudad(long id)
        {
            var ciudad = await _context.Ciudads.FindAsync(id);

            if (ciudad == null)
            {
                return NotFound();
            }

            return ciudad;
        }

        // PUT: api/CiudadControllerApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCiudad(long id, Ciudad ciudad)
        {
            if (id != ciudad.IdC)
            {
                return BadRequest();
            }

            _context.Entry(ciudad).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CiudadExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CiudadControllerApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ciudad>> PostCiudad(Ciudad ciudad)
        {
            _context.Ciudads.Add(ciudad);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCiudad", new { id = ciudad.IdC }, ciudad);
        }

        // DELETE: api/CiudadControllerApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCiudad(long id)
        {
            var ciudad = await _context.Ciudads.FindAsync(id);
            if (ciudad == null)
            {
                return NotFound();
            }

            _context.Ciudads.Remove(ciudad);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CiudadExists(long id)
        {
            return _context.Ciudads.Any(e => e.IdC == id);
        }
    }
}
