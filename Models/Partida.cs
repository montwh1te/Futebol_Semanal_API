using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Futebol_Semanal.Models
{
    public class Partida
    {
        public Partida()
        {
            Estatisticas = new List<Estatistica>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Local { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DataHora { get; set; }

        [StringLength(50)]
        public string CondicoesClimaticas { get; set; }

        [StringLength(100)]
        public string Campeonato { get; set; }

        [Required]
        public int TimeCasaId { get; set; }
        public Time TimeCasa { get; set; }

        [Required]
        public int TimeVisitanteId { get; set; }
        public Time TimeVisitante { get; set; }

        [Range(0, 100)]
        public int PlacarCasa { get; set; }

        [Range(0, 100)]
        public int PlacarVisitante { get; set; }

        public ICollection<Estatistica> Estatisticas { get; set; }
    }
}