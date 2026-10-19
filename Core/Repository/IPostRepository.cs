namespace Core.Repository;

public interface IPostRepository
{
    public void Adicionar(Post post);
    public Post? Buscar(int postId);
    public ICollection<Post> BuscarPostsUsuario(int usuarioId);
    public ICollection<Post> BuscarPostsDeUsuarios(ICollection<int> usuarioIds);
}