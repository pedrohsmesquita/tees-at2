namespace Core;

public class Comentario
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public int PostId { get; set; }
    public string Texto { get; set; } = string.Empty;
    public DateTime CriadoEm { get; set; }
}