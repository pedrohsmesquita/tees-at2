namespace Core.Service;

public interface ICurtidaService
{
    bool Curtir(int usuarioId, int postId);
}