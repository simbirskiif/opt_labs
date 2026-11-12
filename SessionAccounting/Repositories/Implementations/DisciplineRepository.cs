using Microsoft.EntityFrameworkCore;
using Serilog;
using SessionAccounting.Data;
using SessionAccounting.Entities;

namespace SessionAccounting.Repositories.Implementations;

public sealed class DisciplineRepository : IDisciplineRepository
{
    private readonly DataContext _context;
    private readonly ILogger _logger;

    public DisciplineRepository(DataContext context, ILogger logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public IEnumerable<Discipline> ReadDisciplines()
    {
        _logger.Information("Чтение списка дисциплин");
        return _context.Disciplines.AsNoTracking().OrderBy(item => item.Name).ToList();
    }

    public Discipline? ReadDisciplineById(int id)
    {
        _logger.Information("Чтение дисциплины {DisciplineId}", id);
        return _context.Disciplines.AsNoTracking().FirstOrDefault(item => item.Id == id);
    }

    public void CreateDiscipline(Discipline discipline)
    {
        ArgumentNullException.ThrowIfNull(discipline);
        _context.Disciplines.Add(discipline);
        _context.SaveChanges();
        _logger.Information("Создана дисциплина {DisciplineId}: {Name}", discipline.Id, discipline.Name);
    }

    public void UpdateDiscipline(Discipline discipline)
    {
        ArgumentNullException.ThrowIfNull(discipline);
        var existing = _context.Disciplines.Find(discipline.Id)
            ?? throw new InvalidOperationException("Дисциплина не найдена");
        _context.Entry(existing).CurrentValues.SetValues(discipline);
        _context.SaveChanges();
        _logger.Information("Изменена дисциплина {DisciplineId}", discipline.Id);
    }

    public void DeleteDiscipline(int id)
    {
        var discipline = _context.Disciplines.Find(id)
            ?? throw new InvalidOperationException("Дисциплина не найдена");
        _context.Disciplines.Remove(discipline);
        _context.SaveChanges();
        _logger.Information("Удалена дисциплина {DisciplineId}", id);
    }
}
