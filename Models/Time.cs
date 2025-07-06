using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Futebol_Semanal.Models
{
    public class Time
    {
        public Time()
        {
            Jogadores = new List<Jogador>();
            PartidasCasa = new List<Partida>();
            PartidasVisitante = new List<Partida>();
            Estatisticas = new List<Estatistica>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [StringLength(30)]
        public string CorUniforme { get; set; }

        [StringLength(50)]
        public string Cidade { get; set; }

        [StringLength(2)]
        public string Estado { get; set; }

        [DataType(DataType.Date)]
        public DateTime Fundacao { get; set; }

        [StringLength(200)]
        [Url]
        public string? FotoUrl { get; set; }

        [Required]
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public ICollection<Jogador> Jogadores { get; set; }
        public ICollection<Partida> PartidasCasa { get; set; }
        public ICollection<Partida> PartidasVisitante { get; set; }
        public ICollection<Estatistica> Estatisticas { get; set; }
    }
}