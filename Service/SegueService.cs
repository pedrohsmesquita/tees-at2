using Core;
using Core.Exceptions;
using Core.Repository;
using Core.Service;

namespace Service;

public class SegueService : ISegueService
{
    private readonly IUsuarioRepository _usuarios;
    private readonly ISegueRepository _segue;

    public SegueService(IUsuarioRepository usuarios, ISegueRepository segue)
    {
        _usuarios = usuarios;
        _segue = segue;
    }
    
    public bool Seguir(int usuarioId, int alvoId)
    {
        if (usuarioId == alvoId)
            throw new RegraDeNegocioException("Um usuário não pode seguir a si mesmo.");

        GarantirUsuario(usuarioId);
        GarantirUsuario(alvoId);

        return _segue.Adicionar(new Segue(usuarioId, alvoId));
    }

    public ICollection<Usuario> SeguidoresDe(int usuarioId)
    {
        GarantirUsuario(usuarioId);
        return ParaUsuarios(_segue.SeguidoresDe(usuarioId));
    }

    public ICollection<Usuario> SeguindoDe(int usuarioId)
    {
        GarantirUsuario(usuarioId);
        return ParaUsuarios(_segue.SeguindoDe(usuarioId));
    }

    private void GarantirUsuario(int usuarioId)
    {
        if (_usuarios.Buscar(usuarioId) == null)
            throw new NaoEncontradoException($"Usuário {usuarioId} não encontrado.");
    }

    private List<Usuario> ParaUsuarios(IEnumerable<int> ids)
    {
        return ids.Select(_usuarios.Buscar).OfType<Usuario>().ToList();
    }
}