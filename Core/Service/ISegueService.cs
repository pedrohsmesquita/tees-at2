namespace Core.Service;

public interface ISegueService
{
    bool Seguir(int usuarioId, int alvoId);
    ICollection<Usuario> SeguidoresDe(int usuarioId);
    ICollection<Usuario> SeguindoDe(int usuarioId);
}