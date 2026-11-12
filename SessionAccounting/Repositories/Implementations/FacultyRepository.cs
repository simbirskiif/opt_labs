using Microsoft.EntityFrameworkCore;
using Serilog;
using SessionAccounting.Data;
using SessionAccounting.Entities;

namespace SessionAccounting.Repositories.Implementations;

public sealed class FacultyRepository : IFacultyRepository
{
    private readonly DataContext _context;
    private readonly ILogger _logger;

    public FacultyRepository(DataContext context, ILogger logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public IEnumerable<Faculty> ReadFaculties()
    {
        _logger.Information("Чтение списка факультетов");
        return _context.Faculties.AsNoTracking().OrderBy(item => item.Name).ToList();
    }

    public Faculty? ReadFacultyById(int id)
    {
        _logger.Information("Чтение факультета {FacultyId}", id);
        return _context.Faculties.AsNoTracking().FirstOrDefault(item => item.Id == id);
    }

    public void CreateFaculty(Faculty faculty)
    {
        ArgumentNullException.ThrowIfNull(faculty);
        _context.Faculties.Add(faculty);
        _context.SaveChanges();
        _logger.Information("Создан факультет {FacultyId}: {Name}", faculty.Id, faculty.Name);
    }

    public void UpdateFaculty(Faculty faculty)
    {
        ArgumentNullException.ThrowIfNull(faculty);
        var existing = _context.Faculties.Find(faculty.Id)
            ?? throw new InvalidOperationException("Факультет не найден");
        _context.Entry(existing).CurrentValues.SetValues(faculty);
        _context.SaveChanges();
        _logger.Information("Изменён факультет {FacultyId}", faculty.Id);
    }

    public void DeleteFaculty(int id)
    {
        var faculty = _context.Faculties.Find(id)
            ?? throw new InvalidOperationException("Факультет не найден");
        _context.Faculties.Remove(faculty);
        _context.SaveChanges();
        _logger.Information("Удалён факультет {FacultyId}", id);
    }
}
