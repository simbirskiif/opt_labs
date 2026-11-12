using Microsoft.EntityFrameworkCore;
using Serilog;
using SessionAccounting.Data;
using SessionAccounting.Entities;

namespace SessionAccounting.Repositories.Implementations;

public sealed class StudentGroupRepository : IStudentGroupRepository
{
    private readonly DataContext _context;
    private readonly ILogger _logger;

    public StudentGroupRepository(DataContext context, ILogger logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public IEnumerable<StudentGroup> ReadStudentGroups()
    {
        _logger.Information("Чтение списка групп");
        return _context.StudentGroups.AsNoTracking().OrderBy(item => item.Number).ToList();
    }

    public StudentGroup? ReadStudentGroupById(int id)
    {
        _logger.Information("Чтение группы {StudentGroupId}", id);
        return _context.StudentGroups.AsNoTracking().FirstOrDefault(item => item.Id == id);
    }

    public void CreateStudentGroup(StudentGroup group)
    {
        ArgumentNullException.ThrowIfNull(group);
        _context.StudentGroups.Add(group);
        _context.SaveChanges();
        _logger.Information("Создана группа {StudentGroupId}: {Number}", group.Id, group.Number);
    }

    public void UpdateStudentGroup(StudentGroup group)
    {
        ArgumentNullException.ThrowIfNull(group);
        var existing = _context.StudentGroups.Find(group.Id)
            ?? throw new InvalidOperationException("Группа не найдена");
        _context.Entry(existing).CurrentValues.SetValues(group);
        _context.SaveChanges();
        _logger.Information("Изменена группа {StudentGroupId}", group.Id);
    }

    public void DeleteStudentGroup(int id)
    {
        var group = _context.StudentGroups.Find(id)
            ?? throw new InvalidOperationException("Группа не найдена");
        _context.StudentGroups.Remove(group);
        _context.SaveChanges();
        _logger.Information("Удалена группа {StudentGroupId}", id);
    }
}
