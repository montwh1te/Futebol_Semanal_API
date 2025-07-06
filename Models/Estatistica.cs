using System.ComponentModel.DataAnnotations;

namespace Futebol_Semanal.Models
{
    public class Estatistica
    {
        public int Id { get; set; }

        [Required]
        public int PartidaId { get; set; }
        public Partida Partida { get; set; }

        [Required]
        public int JogadorId { get; set; }
        public Jogador Jogador { get; set; }

        [Required]
        public int TimeId { get; set; }
        public Time Time { get; set; }

        [Range(0, 20)]
        public int Gols { get; set; }

        [Range(0, 20)]
        public int Assistencias { get; set; }

        [Range(0, 20)]
        public int Faltas { get; set; }

        [Range(0, 10)]
        public int CartoesAmarelos { get; set; }

        [Range(0, 5)]
        public int CartoesVermelhos { get; set; }

        [Range(0, 150)]
        public int MinutosEmCampo { get; set; }
    }
}