namespace Core.Repository;

public interface IUsuarioRepository
{
    public void Adicionar(Usuario usuario);
    public Usuario? Buscar(int usuarioId);
}