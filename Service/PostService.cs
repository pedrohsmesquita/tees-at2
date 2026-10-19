using Core;
using Core.Exceptions;
using Core.Repository;
using Core.Service;

namespace Service;

public class PostService : IPostService
{
    private readonly IPostRepository _posts;
    private readonly IUsuarioRepository _usuarios;
    private readonly ISegueRepository _segue;
    
    public PostService(IPostRepository posts, IUsuarioRepository usuarios, ISegueRepository segue) {
        _posts = posts;
        _usuarios = usuarios;
        _segue = segue;
    }
    
    public Post Criar(int usuarioId, string texto)
    {
        if (string.IsNullOrWhiteSpace(texto))
            throw new RegraDeNegocioException("O texto do post é obrigatório.");

        GarantirUsuario(usuarioId);

        var post = new Post
        {
            UsuarioId = usuarioId,
            Conteudo = texto,
            CriadoEm = DateTime.UtcNow
        };
        _posts.Adicionar(post);
        return post;
    }

    public Post Buscar(int id)
    {
        var p = _posts.Buscar(id);

        if (p == null)
        {
            throw new NaoEncontradoException($"Post {id} não encontrado.");
        }
        return p;
    }

    public ICollection<Post> PostsDoUsuario(int usuarioId)
    {
        GarantirUsuario(usuarioId);
        return _posts.BuscarPostsUsuario(usuarioId);
    }

    public ICollection<Post> Feed(int usuarioId)
    {
        GarantirUsuario(usuarioId);
        var seguidos = _segue.SeguindoDe(usuarioId);
        return _posts.BuscarPostsDeUsuarios(seguidos);
    }

    private void GarantirUsuario(int usuarioId)
    {
        if (_usuarios.Buscar(usuarioId) == null)
            throw new NaoEncontradoException($"Usuário {usuarioId} não encontrado.");
    }
}