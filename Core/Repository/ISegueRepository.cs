namespace Core.Repository;

public interface ISegueRepository
{
    public bool Adicionar(Segue segue);
    public ICollection<int> SeguidoresDe(int usuarioId);
    public ICollection<int> SeguindoDe(int usuarioId);
}