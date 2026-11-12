using Microsoft.EntityFrameworkCore;
using Serilog;
using SessionAccounting.Data;
using SessionAccounting.Entities;

namespace SessionAccounting.Repositories.Implementations;

public sealed class StudentRepository : IStudentRepository
{
    private readonly DataContext _context;
    private readonly ILogger _logger;

    public StudentRepository(DataContext context, ILogger logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public IEnumerable<Student> ReadStudents()
    {
        _logger.Information("Чтение списка студентов");
        return _context.Students
            .AsNoTracking()
            .OrderBy(item => item.LastName)
            .ThenBy(item => item.FirstName)
            .ToList();
    }

    public Student? ReadStudentById(int id)
    {
        _logger.Information("Чтение студента {StudentId}", id);
        return _context.Students.AsNoTracking().FirstOrDefault(item => item.Id == id);
    }

    public void CreateStudent(Student student)
    {
        ArgumentNullException.ThrowIfNull(student);
        _context.Students.Add(student);
        _context.SaveChanges();
        _logger.Information("Создан студент {StudentId}: {LastName} {FirstName}", student.Id, student.LastName, student.FirstName);
    }

    public void UpdateStudent(Student student)
    {
        ArgumentNullException.ThrowIfNull(student);
        var existing = _context.Students.Find(student.Id)
            ?? throw new InvalidOperationException("Студент не найден");
        _context.Entry(existing).CurrentValues.SetValues(student);
        _context.SaveChanges();
        _logger.Information("Изменён студент {StudentId}", student.Id);
    }

    public void DeleteStudent(int id)
    {
        var student = _context.Students.Find(id)
            ?? throw new InvalidOperationException("Студент не найден");
        _context.Students.Remove(student);
        _context.SaveChanges();
        _logger.Information("Удалён студент {StudentId}", id);
    }
}
