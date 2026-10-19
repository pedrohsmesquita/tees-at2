using System.Collections.Concurrent;
using Core;
using Core.Repository;

namespace Infrastructure;

public class ComentarioRepository : IComentarioRepository
{
    private readonly ConcurrentDictionary<int, Comentario> _dados = new();
    private int _ultimoId = 0;

    public void Adicionar(Comentario comentario)
    {
        comentario.Id = Interlocked.Increment(ref _ultimoId);
        _dados[comentario.Id] = comentario;
    }
}