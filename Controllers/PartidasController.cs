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
    public class PartidasController : ControllerBase
    {
        private readonly PostgresDbContext _context;

        public PartidasController(PostgresDbContext context)
        {
            _context = context;
        }

        // GET: api/Partidas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Partida>>> GetPartidas()
        {
            return await _context.Partidas.ToListAsync();
        }

        // GET: api/Partidas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Partida>> GetPartida(int id)
        {
            var partida = await _context.Partidas.FindAsync(id);

            if (partida == null)
            {
                return NotFound();
            }

            return partida;
        }

        // PATCH: api/Partidas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchPartida(int id, [FromBody] PartidaPatchDto dto)
        {
            var partida = await _context.Partidas.FindAsync(id);
            if (partida == null)
                return NotFound();

            if (dto.Local != null)
                partida.Local = dto.Local;
            if (dto.DataHora.HasValue)
                partida.DataHora = DateTime.SpecifyKind(dto.DataHora.Value, DateTimeKind.Utc);
            if (dto.CondicoesClimaticas != null)
                partida.CondicoesClimaticas = dto.CondicoesClimaticas;
            if (dto.Campeonato != null)
                partida.Campeonato = dto.Campeonato;
            if (dto.TimeCasaId.HasValue)
                partida.TimeCasaId = dto.TimeCasaId.Value;
            if (dto.TimeVisitanteId.HasValue)
                partida.TimeVisitanteId = dto.TimeVisitanteId.Value;
            if (dto.PlacarCasa.HasValue)
                partida.PlacarCasa = dto.PlacarCasa.Value;
            if (dto.PlacarVisitante.HasValue)
                partida.PlacarVisitante = dto.PlacarVisitante.Value;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/Partidas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Partida>> PostTime(PartidaCreateDto dto)
        {
            var partida = new Partida
            {
                Local = dto.Local,
                DataHora = DateTime.SpecifyKind(dto.DataHora, DateTimeKind.Utc),
                CondicoesClimaticas = dto.CondicoesClimaticas,
                Campeonato = dto.Campeonato,
                TimeCasaId = dto.TimeCasaId,
                TimeVisitanteId = dto.TimeVisitanteId,
                PlacarCasa = dto.PlacarCasa,
                PlacarVisitante = dto.PlacarVisitante
            };

            _context.Partidas.Add(partida);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPartida", new { id = partida.Id }, partida);
        }

        // DELETE: api/Partidas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePartida(int id)
        {
            var partida = await _context.Partidas.FindAsync(id);
            if (partida == null)
            {
                return NotFound();
            }

            _context.Partidas.Remove(partida);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
