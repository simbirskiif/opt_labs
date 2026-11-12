using Microsoft.EntityFrameworkCore;
using Serilog;
using SessionAccounting.Data;
using SessionAccounting.Entities;

namespace SessionAccounting.Repositories.Implementations;

public sealed class TeacherRepository : ITeacherRepository
{
    private readonly DataContext _context;
    private readonly ILogger _logger;

    public TeacherRepository(DataContext context, ILogger logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public IEnumerable<Teacher> ReadTeachers()
    {
        _logger.Information("Чтение списка преподавателей");
        return _context.Teachers
            .AsNoTracking()
            .OrderBy(item => item.LastName)
            .ThenBy(item => item.FirstName)
            .ToList();
    }

    public Teacher? ReadTeacherById(int id)
    {
        _logger.Information("Чтение преподавателя {TeacherId}", id);
        return _context.Teachers.AsNoTracking().FirstOrDefault(item => item.Id == id);
    }

    public void CreateTeacher(Teacher teacher)
    {
        ArgumentNullException.ThrowIfNull(teacher);
        _context.Teachers.Add(teacher);
        _context.SaveChanges();
        _logger.Information("Создан преподаватель {TeacherId}: {LastName} {FirstName}", teacher.Id, teacher.LastName, teacher.FirstName);
    }

    public void UpdateTeacher(Teacher teacher)
    {
        ArgumentNullException.ThrowIfNull(teacher);
        var existing = _context.Teachers.Find(teacher.Id)
            ?? throw new InvalidOperationException("Преподаватель не найден");
        _context.Entry(existing).CurrentValues.SetValues(teacher);
        _context.SaveChanges();
        _logger.Information("Изменён преподаватель {TeacherId}", teacher.Id);
    }

    public void DeleteTeacher(int id)
    {
        var teacher = _context.Teachers.Find(id)
            ?? throw new InvalidOperationException("Преподаватель не найден");
        _context.Teachers.Remove(teacher);
        _context.SaveChanges();
        _logger.Information("Удалён преподаватель {TeacherId}", id);
    }
}
