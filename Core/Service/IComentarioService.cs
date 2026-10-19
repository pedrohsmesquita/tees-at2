namespace Core.Service;

public interface IComentarioService
{
    Comentario Comentar(int usuarioId, int postId, string texto);
}