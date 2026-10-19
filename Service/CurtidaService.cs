using Core;
using Core.Exceptions;
using Core.Repository;
using Core.Service;

namespace Service;

public class CurtidaService : ICurtidaService
{
    private readonly IUsuarioRepository _usuarios;
    private readonly ICurtidaRepository _curtidas;
    private readonly IPostRepository _posts;

    public CurtidaService(IUsuarioRepository usuarios, ICurtidaRepository curtidas, IPostRepository posts)
    {
        _usuarios = usuarios;
        _curtidas = curtidas;
        _posts = posts;
    }
    
    public bool Curtir(int usuarioId, int postId)
    {
        if (_usuarios.Buscar(usuarioId) == null)
            throw new NaoEncontradoException($"Usuário {usuarioId} não encontrado.");
        if (_posts.Buscar(postId) == null)
            throw new NaoEncontradoException($"Post {postId} não encontrado.");

        return _curtidas.Adicionar(new Curtida(usuarioId, postId));
    }
}