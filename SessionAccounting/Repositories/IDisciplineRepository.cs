using SessionAccounting.Entities;

namespace SessionAccounting.Repositories;

public interface IDisciplineRepository
{
    IEnumerable<Discipline> ReadDisciplines();

    Discipline? ReadDisciplineById(int id);

    void CreateDiscipline(Discipline discipline);

    void UpdateDiscipline(Discipline discipline);

    void DeleteDiscipline(int id);
}
