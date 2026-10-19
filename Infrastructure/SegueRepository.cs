using System.Collections.Concurrent;
using Core;
using Core.Repository;

namespace Infrastructure;

public class SegueRepository : ISegueRepository
{
    private readonly ConcurrentDictionary<(int SeguidorId, int SeguidoId), Segue> _dados = new();

    public bool Adicionar(Segue segue)
    {
        return _dados.TryAdd((segue.SeguidorId, segue.SeguidoId), segue);
    }

    public ICollection<int> SeguidoresDe(int usuarioId)
    {
        return _dados.Keys
            .Where(k => k.SeguidoId == usuarioId)
            .Select(k => k.SeguidorId)
            .ToList();
    }

    public ICollection<int> SeguindoDe(int usuarioId)
    {
        return _dados.Keys
            .Where(k => k.SeguidorId == usuarioId)
            .Select(k => k.SeguidoId)
            .ToList();
    }
}