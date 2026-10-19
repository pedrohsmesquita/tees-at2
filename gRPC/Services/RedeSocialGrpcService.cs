using Core;
using Core.Service;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using TEES_AT2.Protos;

namespace TEES_AT2.Services;

public class RedeSocialGrpcService(
    IUsuarioService usuarios,
    IPostService posts,
    IComentarioService comentarios,
    ICurtidaService curtidas,
    ISegueService segues) : RedeSocial.RedeSocialBase
{
    public override Task<UsuarioMsg> CriarUsuario(CriarUsuarioRequest r, ServerCallContext c)
        => Task.FromResult(Mapear(usuarios.Criar(r.Nome, r.Email)));

    public override Task<PostMsg> CriarPost(CriarPostRequest r, ServerCallContext c)
        => Task.FromResult(Mapear(posts.Criar(r.UsuarioId, r.Texto)));

    public override Task<ComentarioMsg> Comentar(ComentarRequest r, ServerCallContext c)
        => Task.FromResult(Mapear(comentarios.Comentar(r.UsuarioId, r.PostId, r.Texto)));

    public override Task<ResultadoRelacao> Curtir(RelacaoRequest r, ServerCallContext c)
        => Task.FromResult(new ResultadoRelacao { Criado = curtidas.Curtir(r.UsuarioId, r.AlvoId) });

    public override Task<ResultadoRelacao> Seguir(RelacaoRequest r, ServerCallContext c)
        => Task.FromResult(new ResultadoRelacao { Criado = segues.Seguir(r.UsuarioId, r.AlvoId) });

    public override Task<UsuarioMsg> BuscarUsuario(IdRequest r, ServerCallContext c)
        => Task.FromResult(Mapear(usuarios.Buscar(r.Id)));

    public override Task<PostMsg> BuscarPost(IdRequest r, ServerCallContext c)
        => Task.FromResult(Mapear(posts.Buscar(r.Id)));

    public override Task<ListaPosts> PostsDoUsuario(IdRequest r, ServerCallContext c)
        => Task.FromResult(MapearPosts(posts.PostsDoUsuario(r.Id)));

    public override Task<ListaPosts> FeedDoUsuario(IdRequest r, ServerCallContext c)
        => Task.FromResult(MapearPosts(posts.Feed(r.Id)));

    public override Task<ListaUsuarios> SeguidoresDe(IdRequest r, ServerCallContext c)
        => Task.FromResult(MapearUsuarios(segues.SeguidoresDe(r.Id)));

    public override Task<ListaUsuarios> SeguindoDe(IdRequest r, ServerCallContext c)
        => Task.FromResult(MapearUsuarios(segues.SeguindoDe(r.Id)));

    private static UsuarioMsg Mapear(Usuario u) => new()
    {
        Id = u.Id, Nome = u.Nome, Email = u.Email, Biografia = u.Biografia
    };

    private static PostMsg Mapear(Post p) => new()
    {
        Id = p.Id,
        UsuarioId = p.UsuarioId,
        Conteudo = p.Conteudo,
        CriadoEm = Timestamp.FromDateTime(DateTime.SpecifyKind(p.CriadoEm, DateTimeKind.Utc))
    };

    private static ComentarioMsg Mapear(Comentario c) => new()
    {
        Id = c.Id, UsuarioId = c.UsuarioId, PostId = c.PostId, Texto = c.Texto
    };

    private static ListaPosts MapearPosts(IEnumerable<Post> posts)
    {
        var lista = new ListaPosts();
        lista.Posts.AddRange(posts.Select(Mapear));
        return lista;
    }

    private static ListaUsuarios MapearUsuarios(IEnumerable<Usuario> usuarios)
    {
        var lista = new ListaUsuarios();
        lista.Usuarios.AddRange(usuarios.Select(Mapear));
        return lista;
    }
}