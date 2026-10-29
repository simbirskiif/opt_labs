using SessionAccounting.Entities;

namespace SessionAccounting.Repositories;

public interface IStudentGroupRepository
{
    IEnumerable<StudentGroup> ReadStudentGroups();

    StudentGroup? ReadStudentGroupById(int id);

    void CreateStudentGroup(StudentGroup group);

    void UpdateStudentGroup(StudentGroup group);

    void DeleteStudentGroup(int id);
}
