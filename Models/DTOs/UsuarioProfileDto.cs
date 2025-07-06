public class UsuarioProfileDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public DateTime DataCadastro { get; set; }
    public DateTime? UltimoLogin { get; set; }
}