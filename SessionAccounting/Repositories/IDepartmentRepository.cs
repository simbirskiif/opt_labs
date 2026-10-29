using SessionAccounting.Entities;

namespace SessionAccounting.Repositories;

public interface IDepartmentRepository
{
    IEnumerable<Department> ReadDepartments();

    Department? ReadDepartmentById(int id);

    void CreateDepartment(Department department);

    void UpdateDepartment(Department department);

    void DeleteDepartment(int id);
}
