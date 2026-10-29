using SessionAccounting.Entities;

namespace SessionAccounting.Repositories.Implementations;

public sealed class StudentGroupRepository : IStudentGroupRepository
{
    public IEnumerable<StudentGroup> ReadStudentGroups()
    {
        return [];
    }

    public StudentGroup? ReadStudentGroupById(int id)
    {
        return null;
    }

    public void CreateStudentGroup(StudentGroup group)
    {
    }

    public void UpdateStudentGroup(StudentGroup group)
    {
    }

    public void DeleteStudentGroup(int id)
    {
    }
}
