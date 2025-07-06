using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Futebol_Semanal.Models
{
    public class Jogador
    {
        public Jogador()
        {
            Estatisticas = new List<Estatistica>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [StringLength(200)]
        [Url]
        public string? ImagemUrl { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        [Range(1.0, 2.5)]
        public double Altura { get; set; }

        [Range(30, 200)]
        public double Peso { get; set; }

        [StringLength(30)]
        public string Posicao { get; set; }

        [Range(1, 99)]
        public int NumeroCamisa { get; set; }

        [Required]
        public int TimeId { get; set; }
        public Time Time { get; set; }

        [Required]
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public ICollection<Estatistica> Estatisticas { get; set; }
    }
}