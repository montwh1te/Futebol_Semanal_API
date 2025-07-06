using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Futebol_Semanal.Models
{
    public class Usuario
    {
        public Usuario()
        {
            Times = new List<Time>();
            Jogadores = new List<Jogador>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Phone]
        [StringLength(20)]
        public string Telefone { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DataCadastro { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? UltimoLogin { get; set; }

        public bool Ativo { get; set; }

        public ICollection<Time> Times { get; set; }
        public ICollection<Jogador> Jogadores { get; set; }
    }
}