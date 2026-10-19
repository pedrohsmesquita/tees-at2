using System.Collections.Concurrent;
using Core;
using Core.Repository;

namespace Infrastructure;

public class PostRepository : IPostRepository
{
    private readonly ConcurrentDictionary<int, Post> _dados = new();
    private int _ultimoId = 0;

    public void Adicionar(Post post)
    {
        post.Id = Interlocked.Increment(ref _ultimoId);
        _dados[post.Id] = post;
    }

    public Post? Buscar(int postId)
    {
        return _dados.GetValueOrDefault(postId);
    }

    public ICollection<Post> BuscarPostsUsuario(int usuarioId)
    {
        return _dados.Values
            .Where(p => p.UsuarioId == usuarioId)
            .OrderByDescending(p => p.CriadoEm)
            .ToList();
    }

    public ICollection<Post> BuscarPostsDeUsuarios(ICollection<int> usuarioIds)
    {
        var ids = usuarioIds.ToHashSet();   // lookup O(1) em vez de Contains em lista

        return _dados.Values
            .Where(p => ids.Contains(p.UsuarioId))
            .OrderByDescending(p => p.CriadoEm)
            .ToList();
    }
}