namespace Core;

public class Curtida
{
    public int UsuarioId { get; set; }
    public int PostId { get; set; }

    public Curtida(int usuarioId, int postId)
    {
        UsuarioId = usuarioId;
        PostId = postId;
    }
}