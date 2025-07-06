using Futebol_Semanal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Futebol_Semanal_API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class EstatisticasController : ControllerBase
    {
        private readonly PostgresDbContext _context;

        public EstatisticasController(PostgresDbContext context)
        {
            _context = context;
        }

        // GET: api/Estatisticas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Estatistica>>> GetEstatisticas()
        {
            return await _context.Estatisticas.ToListAsync();
        }

        // GET: api/Estatisticas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Estatistica>> GetEstatistica(int id)
        {
            var estatistica = await _context.Estatisticas.FindAsync(id);

            if (estatistica == null)
            {
                return NotFound();
            }

            return estatistica;
        }

        // PATCH: api/Estatisticas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchEstatistica(int id, [FromBody] EstatisticaPatchDto dto)
        {
            var estatistica = await _context.Estatisticas.FindAsync(id);
            if (estatistica == null)
                return NotFound();

            if (dto.Gols.HasValue)
                estatistica.Gols = dto.Gols.Value;
            if (dto.Assistencias.HasValue)
                estatistica.Assistencias = dto.Assistencias.Value;
            if (dto.Faltas.HasValue)
                estatistica.Faltas = dto.Faltas.Value;
            if (dto.CartoesAmarelos.HasValue)
                estatistica.CartoesAmarelos = dto.CartoesAmarelos.Value;
            if (dto.CartoesVermelhos.HasValue)
                estatistica.CartoesVermelhos = dto.CartoesVermelhos.Value;
            if (dto.MinutosEmCampo.HasValue)
                estatistica.MinutosEmCampo = dto.MinutosEmCampo.Value;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/Estatisticas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Estatistica>> PostTime(EstatisticaCreateDto dto)
        {
            var estatistica = new Estatistica
            {
                PartidaId = dto.PartidaId,
                JogadorId = dto.JogadorId,
                TimeId = dto.TimeId,
                Gols = dto.Gols,
                Assistencias = dto.Assistencias,
                Faltas = dto.Faltas,
                CartoesAmarelos = dto.CartoesAmarelos,
                CartoesVermelhos = dto.CartoesVermelhos,
                MinutosEmCampo = dto.MinutosEmCampo
            };

            _context.Estatisticas.Add(estatistica);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEstatistica", new { id = estatistica.Id }, estatistica);
        }

        // DELETE: api/Estatisticas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstatistica(int id)
        {
            var estatistica = await _context.Estatisticas.FindAsync(id);
            if (estatistica == null)
            {
                return NotFound();
            }

            _context.Estatisticas.Remove(estatistica);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
