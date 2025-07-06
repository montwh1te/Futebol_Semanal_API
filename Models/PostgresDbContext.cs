using Microsoft.EntityFrameworkCore;

namespace Futebol_Semanal.Models
{
    public class PostgresDbContext : DbContext
    {
        public PostgresDbContext(DbContextOptions<PostgresDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Time> Times { get; set; }
        public DbSet<Jogador> Jogadores { get; set; }
        public DbSet<Partida> Partidas { get; set; }
        public DbSet<Estatistica> Estatisticas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relacionamento TimeCasa
            modelBuilder.Entity<Partida>()
                .HasOne(p => p.TimeCasa)
                .WithMany(t => t.PartidasCasa)
                .HasForeignKey(p => p.TimeCasaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento TimeVisitante
            modelBuilder.Entity<Partida>()
                .HasOne(p => p.TimeVisitante)
                .WithMany(t => t.PartidasVisitante)
                .HasForeignKey(p => p.TimeVisitanteId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}