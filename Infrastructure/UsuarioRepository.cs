using System.Collections.Concurrent;
using Core;
using Core.Repository;

namespace Infrastructure;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly ConcurrentDictionary<int, Usuario> _dados = new();
    private int _ultimoId = 0;
    
    public void Adicionar(Usuario usuario)
    {
        usuario.Id = Interlocked.Increment(ref _ultimoId);
        _dados[usuario.Id] = usuario;
    }

    public Usuario? Buscar(int usuarioId)
    {
        return _dados.GetValueOrDefault(usuarioId);
    }
}