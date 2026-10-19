using Core;
using Core.Exceptions;
using Core.Repository;
using Core.Service;

namespace Service;

public class ComentarioService : IComentarioService
{
    private readonly IUsuarioRepository _usuarios;
    private readonly IComentarioRepository _comentarios;
    private readonly IPostRepository _posts;

    public ComentarioService(IUsuarioRepository usuarios, IComentarioRepository comentarios, IPostRepository posts)
    {
        _usuarios = usuarios;
        _comentarios = comentarios;
        _posts = posts;
    }
    
    public Comentario Comentar(int usuarioId, int postId, string texto)
    {
        if (string.IsNullOrWhiteSpace(texto))
            throw new RegraDeNegocioException("O texto do comentário é obrigatório.");
        if (_usuarios.Buscar(usuarioId) == null)
            throw new NaoEncontradoException($"Usuário {usuarioId} não encontrado.");
        if (_posts.Buscar(postId) == null)
            throw new NaoEncontradoException($"Post {postId} não encontrado.");

        var comentario = new Comentario
        {
            UsuarioId = usuarioId,
            PostId = postId,
            Texto = texto
        };
        _comentarios.Adicionar(comentario);
        return comentario;
    }
}