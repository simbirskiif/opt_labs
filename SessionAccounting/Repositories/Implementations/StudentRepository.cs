using SessionAccounting.Entities;

namespace SessionAccounting.Repositories.Implementations;

public sealed class StudentRepository : IStudentRepository
{
    public IEnumerable<Student> ReadStudents()
    {
        return [];
    }

    public Student? ReadStudentById(int id)
    {
        return null;
    }

    public void CreateStudent(Student student)
    {
    }

    public void UpdateStudent(Student student)
    {
    }

    public void DeleteStudent(int id)
    {
    }
}
