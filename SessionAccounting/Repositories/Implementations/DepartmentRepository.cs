using SessionAccounting.Entities;

namespace SessionAccounting.Repositories.Implementations;

public sealed class DepartmentRepository : IDepartmentRepository
{
    public IEnumerable<Department> ReadDepartments()
    {
        return [];
    }

    public Department? ReadDepartmentById(int id)
    {
        return null;
    }

    public void CreateDepartment(Department department)
    {
    }

    public void UpdateDepartment(Department department)
    {
    }

    public void DeleteDepartment(int id)
    {
    }
}
