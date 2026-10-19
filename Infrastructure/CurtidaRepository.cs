using System.Collections.Concurrent;

namespace Infrastructure;
using Core;
using Core.Repository;

public class CurtidaRepository : ICurtidaRepository
{
    private readonly ConcurrentDictionary<(int UsuarioId, int PostId), Curtida> _dados = new();

    public bool Adicionar(Curtida curtida)
    {
        return _dados.TryAdd((curtida.UsuarioId, curtida.PostId), curtida);
    }
}