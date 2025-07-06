using Futebol_Semanal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using System.IO;

namespace Futebol_Semanal_API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class JogadoresController : ControllerBase
    {
        private readonly PostgresDbContext _context;
        private readonly IConfiguration _configuration;

        public JogadoresController(PostgresDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: api/Jogadores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Jogador>>> GetJogadores()
        {
            return await _context.Jogadores.ToListAsync();
        }

        // GET: api/Jogadores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Jogador>> GetJogador(int id)
        {
            var jogador = await _context.Jogadores.FindAsync(id);

            if (jogador == null)
            {
                return NotFound();
            }

            return jogador;
        }

        // PATCH: api/Jogadores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchJogador(int id, [FromBody] JogadorPatchDto dto)
        {
            var jogador = await _context.Jogadores.FindAsync(id);
            if (jogador == null)
                return NotFound();

            if (dto.Nome != null)
                jogador.Nome = dto.Nome;
            if (dto.ImagemUrl != null)
                jogador.ImagemUrl = dto.ImagemUrl;
            if (dto.DataNascimento.HasValue)
                jogador.DataNascimento = DateTime.SpecifyKind(dto.DataNascimento.Value, DateTimeKind.Utc);
            if (dto.Altura.HasValue)
                jogador.Altura = dto.Altura.Value;
            if (dto.Peso.HasValue)
                jogador.Peso = dto.Peso.Value;
            if (dto.Posicao != null)
                jogador.Posicao = dto.Posicao;
            if (dto.NumeroCamisa.HasValue)
                jogador.NumeroCamisa = dto.NumeroCamisa.Value;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/Jogadores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Jogador>> PostTime(JogadorCreateDto dto)
        {
            var jogador = new Jogador
            {
                Nome = dto.Nome,
                ImagemUrl = null,
                DataNascimento = DateTime.SpecifyKind(dto.DataNascimento, DateTimeKind.Utc),
                Altura = dto.Altura,
                Peso = dto.Peso,
                Posicao = dto.Posicao,
                NumeroCamisa = dto.NumeroCamisa,
                TimeId = dto.TimeId,
                UsuarioId = dto.UsuarioId
            };

            _context.Jogadores.Add(jogador);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJogador", new { id = jogador.Id }, jogador);
        }

        // DELETE: api/Jogadores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJogador(int id)
        {
            var jogador = await _context.Jogadores.FindAsync(id);
            if (jogador == null)
            {
                return NotFound();
            }

            _context.Jogadores.Remove(jogador);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Jogadores/{id}/upload-foto
        [HttpPost("{id}/upload-foto")]
        public async Task<IActionResult> UploadFoto(int id, IFormFile foto)
        {
            var jogador = await _context.Jogadores.FindAsync(id);
            if (jogador == null)
                return NotFound();

            if (foto == null || foto.Length == 0)
                return BadRequest("Arquivo inválido.");

            var connectionString = _configuration.GetConnectionString("AzureBlobStorage");

            // Remover imagem antiga do Azure Blob Storage, se existir
            if (!string.IsNullOrEmpty(jogador.ImagemUrl))
            {
                var oldUri = new Uri(jogador.ImagemUrl);
                var oldBlobName = Path.GetFileName(oldUri.LocalPath);

                var blobServiceClient = new BlobServiceClient(connectionString);
                var containerClient = blobServiceClient.GetBlobContainerClient("fotos-jogadores");
                var oldBlobClient = containerClient.GetBlobClient(oldBlobName);

                await oldBlobClient.DeleteIfExistsAsync();
            }

            // Upload da nova imagem
            var blobService = new BlobServiceClient(connectionString);
            var container = blobService.GetBlobContainerClient("fotos-jogadores");
            await container.CreateIfNotExistsAsync();

            var newBlobName = $"{Guid.NewGuid()}_{foto.FileName}";
            var blobClient = container.GetBlobClient(newBlobName);

            using (var stream = foto.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, true);
            }

            jogador.ImagemUrl = blobClient.Uri.ToString();
            await _context.SaveChangesAsync();

            return Ok(new { url = jogador.ImagemUrl });
        }
    }
}
