using Microsoft.EntityFrameworkCore;
using Serilog;
using SessionAccounting.Data;
using SessionAccounting.Entities;

namespace SessionAccounting.Repositories.Implementations;

public sealed class DepartmentRepository : IDepartmentRepository
{
    private readonly DataContext _context;
    private readonly ILogger _logger;

    public DepartmentRepository(DataContext context, ILogger logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public IEnumerable<Department> ReadDepartments()
    {
        _logger.Information("Чтение списка кафедр");
        return _context.Departments.AsNoTracking().OrderBy(item => item.Name).ToList();
    }

    public Department? ReadDepartmentById(int id)
    {
        _logger.Information("Чтение кафедры {DepartmentId}", id);
        return _context.Departments.AsNoTracking().FirstOrDefault(item => item.Id == id);
    }

    public void CreateDepartment(Department department)
    {
        ArgumentNullException.ThrowIfNull(department);
        _context.Departments.Add(department);
        _context.SaveChanges();
        _logger.Information("Создана кафедра {DepartmentId}: {Name}", department.Id, department.Name);
    }

    public void UpdateDepartment(Department department)
    {
        ArgumentNullException.ThrowIfNull(department);
        var existing = _context.Departments.Find(department.Id)
            ?? throw new InvalidOperationException("Кафедра не найдена");
        _context.Entry(existing).CurrentValues.SetValues(department);
        _context.SaveChanges();
        _logger.Information("Изменена кафедра {DepartmentId}", department.Id);
    }

    public void DeleteDepartment(int id)
    {
        var department = _context.Departments.Find(id)
            ?? throw new InvalidOperationException("Кафедра не найдена");
        _context.Departments.Remove(department);
        _context.SaveChanges();
        _logger.Information("Удалена кафедра {DepartmentId}", id);
    }
}
