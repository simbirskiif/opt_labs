using SessionAccounting.Entities;

namespace SessionAccounting.Repositories;

public interface ITeacherRepository
{
    IEnumerable<Teacher> ReadTeachers();

    Teacher? ReadTeacherById(int id);

    void CreateTeacher(Teacher teacher);

    void UpdateTeacher(Teacher teacher);

    void DeleteTeacher(int id);
}
