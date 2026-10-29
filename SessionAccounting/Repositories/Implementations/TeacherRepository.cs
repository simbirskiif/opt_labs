using SessionAccounting.Entities;

namespace SessionAccounting.Repositories.Implementations;

public sealed class TeacherRepository : ITeacherRepository
{
    public IEnumerable<Teacher> ReadTeachers()
    {
        return [];
    }

    public Teacher? ReadTeacherById(int id)
    {
        return null;
    }

    public void CreateTeacher(Teacher teacher)
    {
    }

    public void UpdateTeacher(Teacher teacher)
    {
    }

    public void DeleteTeacher(int id)
    {
    }
}
