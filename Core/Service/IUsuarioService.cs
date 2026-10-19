namespace Core.Service;

public interface IUsuarioService
{
    Usuario Criar(string nome, string email);
    Usuario Buscar(int id);
}