namespace Core;

public class Post
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public string Conteudo { get; set; } = string.Empty;
    public DateTime CriadoEm { get; set; }
}