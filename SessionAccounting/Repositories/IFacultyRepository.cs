using SessionAccounting.Entities;

namespace SessionAccounting.Repositories;

public interface IFacultyRepository
{
    IEnumerable<Faculty> ReadFaculties();

    Faculty? ReadFacultyById(int id);

    void CreateFaculty(Faculty faculty);

    void UpdateFaculty(Faculty faculty);

    void DeleteFaculty(int id);
}
