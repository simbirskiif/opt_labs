using SessionAccounting.Entities;

namespace SessionAccounting.Repositories.Implementations;

public sealed class FacultyRepository : IFacultyRepository
{
    public IEnumerable<Faculty> ReadFaculties()
    {
        return [];
    }

    public Faculty? ReadFacultyById(int id)
    {
        return null;
    }

    public void CreateFaculty(Faculty faculty)
    {
    }

    public void UpdateFaculty(Faculty faculty)
    {
    }

    public void DeleteFaculty(int id)
    {
    }
}
