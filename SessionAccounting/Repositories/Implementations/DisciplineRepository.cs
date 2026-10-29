using SessionAccounting.Entities;

namespace SessionAccounting.Repositories.Implementations;

public sealed class DisciplineRepository : IDisciplineRepository
{
    public IEnumerable<Discipline> ReadDisciplines()
    {
        return [];
    }

    public Discipline? ReadDisciplineById(int id)
    {
        return null;
    }

    public void CreateDiscipline(Discipline discipline)
    {
    }

    public void UpdateDiscipline(Discipline discipline)
    {
    }

    public void DeleteDiscipline(int id)
    {
    }
}
