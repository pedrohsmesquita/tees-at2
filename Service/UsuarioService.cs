using Core;
using Core.Exceptions;
using Core.Repository;
using Core.Service;

namespace Service;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarios;

    public UsuarioService(IUsuarioRepository usuarios)
    {
        _usuarios = usuarios;
    }
        
    public Usuario Criar(string nome, string email)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new RegraDeNegocioException("O nome é obrigatório.");
        if (string.IsNullOrWhiteSpace(email))
            throw new RegraDeNegocioException("O e-mail é obrigatório.");
        var usuario = new Usuario { Nome = nome.Trim(), Email = email.Trim() };
        _usuarios.Adicionar(usuario);
        return usuario;
    }

    public Usuario Buscar(int id)
    {
        var usuario = _usuarios.Buscar(id);
        
        if (usuario == null)
        {
            throw new NaoEncontradoException($"Usuário {id} não encontrado.");
        }
        
        return usuario;
    }
}