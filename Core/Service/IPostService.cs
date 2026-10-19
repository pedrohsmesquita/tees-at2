namespace Core.Service;

public interface IPostService
{
    Post Criar(int usuarioId, string texto);
    Post Buscar(int id);
    ICollection<Post> PostsDoUsuario(int usuarioId);
    ICollection<Post> Feed(int usuarioId);
}