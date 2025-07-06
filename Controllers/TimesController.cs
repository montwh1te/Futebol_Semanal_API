using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Futebol_Semanal.Models;
using Microsoft.AspNetCore.Authorization;
using Azure.Storage.Blobs;
using System.IO;

namespace Futebol_Semanal_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TimesController : ControllerBase
    {
        private readonly PostgresDbContext _context;
        private readonly IConfiguration _configuration; 

        public TimesController(PostgresDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

        }

        // GET: api/Times
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Time>>> GetTimes()
        {
            return await _context.Times.ToListAsync();
        }

        // GET: api/Times/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Time>> GetTime(int id)
        {
            var time = await _context.Times.FindAsync(id);

            if (time == null)
            {
                return NotFound();
            }

            return time;
        }

        // PATCH: api/Times/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchTime(int id, [FromBody] TimePatchDto dto)
        {
            var time = await _context.Times.FindAsync(id);
            if (time == null)
                return NotFound();

            if (dto.Nome != null)
                time.Nome = dto.Nome;
            if (dto.CorUniforme != null)
                time.CorUniforme = dto.CorUniforme;
            if (dto.Cidade != null)
                time.Cidade = dto.Cidade;
            if (dto.Estado != null)
                time.Estado = dto.Estado;
            if (dto.Fundacao.HasValue)
                time.Fundacao = DateTime.SpecifyKind(dto.Fundacao.Value, DateTimeKind.Utc);
            if (dto.FotoUrl != null)
                time.FotoUrl = dto.FotoUrl;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/Times
        [HttpPost]
        public async Task<ActionResult<Time>> PostTime(TimeCreateDto dto)
        {
            var time = new Time
            {
                Nome = dto.Nome,
                CorUniforme = dto.CorUniforme,
                Cidade = dto.Cidade,
                Estado = dto.Estado,
                Fundacao = DateTime.SpecifyKind(dto.Fundacao, DateTimeKind.Utc),
                UsuarioId = dto.UsuarioId,
                FotoUrl = null
            };

            _context.Times.Add(time);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTime", new { id = time.Id }, time);
        }

        // DELETE: api/Times/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTime(int id)
        {
            var time = await _context.Times.FindAsync(id);
            if (time == null)
            {
                return NotFound();
            }

            _context.Times.Remove(time);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Times/{id}/upload-foto
        [HttpPost("{id}/upload-foto")]
        public async Task<IActionResult> UploadFoto(int id, IFormFile foto)
        {
            var time = await _context.Times.FindAsync(id);
            if (time == null)
                return NotFound();

            if (foto == null || foto.Length == 0)
                return BadRequest("Arquivo inválido.");

            var connectionString = _configuration.GetConnectionString("AzureBlobStorage");

            // Remover imagem antiga do Azure Blob Storage, se existir
            if (!string.IsNullOrEmpty(time.FotoUrl))
            {
                var oldUri = new Uri(time.FotoUrl);
                var oldBlobName = Path.GetFileName(oldUri.LocalPath);

                var blobServiceClient = new BlobServiceClient(connectionString);
                var containerClient = blobServiceClient.GetBlobContainerClient("fotos-times");
                var oldBlobClient = containerClient.GetBlobClient(oldBlobName);

                await oldBlobClient.DeleteIfExistsAsync();
            }

            // Upload da nova imagem
            var blobService = new BlobServiceClient(connectionString);
            var container = blobService.GetBlobContainerClient("fotos-times");
            await container.CreateIfNotExistsAsync();

            var newBlobName = $"{Guid.NewGuid()}_{foto.FileName}";
            var blobClient = container.GetBlobClient(newBlobName);

            using (var stream = foto.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, true);
            }

            time.FotoUrl = blobClient.Uri.ToString();
            await _context.SaveChangesAsync();

            return Ok(new { url = time.FotoUrl });
        }

        
    }
}
