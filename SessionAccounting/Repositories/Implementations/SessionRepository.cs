using Microsoft.EntityFrameworkCore;
using Serilog;
using SessionAccounting.Data;
using SessionAccounting.Entities;

namespace SessionAccounting.Repositories.Implementations;

public sealed class SessionRepository : ISessionRepository
{
    private readonly DataContext _context;
    private readonly ILogger _logger;

    public SessionRepository(DataContext context, ILogger logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public IEnumerable<Session> ReadSessions(
        DateTime? dateFrom = null,
        DateTime? dateTo = null,
        int? studentId = null,
        int? groupId = null)
    {
        _logger.Information(
            "Чтение сессий: с {DateFrom}, по {DateTo}, студент {StudentId}, группа {GroupId}",
            dateFrom,
            dateTo,
            studentId,
            groupId);

        var query = _context.Sessions
            .Include(item => item.SessionDisciplines)
            .AsNoTracking()
            .AsQueryable();

        if (dateFrom.HasValue)
        {
            query = query.Where(item => item.SessionDate >= dateFrom.Value);
        }

        if (dateTo.HasValue)
        {
            query = query.Where(item => item.SessionDate <= dateTo.Value);
        }

        if (studentId.HasValue)
        {
            query = query.Where(item => item.StudentId == studentId.Value);
        }

        if (groupId.HasValue)
        {
            query = query.Where(item => _context.Students.Any(student =>
                student.Id == item.StudentId && student.GroupId == groupId.Value));
        }

        return query.OrderByDescending(item => item.SessionDate).ToList();
    }

    public Session? ReadSessionById(int id)
    {
        _logger.Information("Чтение сессии {SessionId}", id);
        return _context.Sessions
            .Include(item => item.SessionDisciplines)
            .AsNoTracking()
            .FirstOrDefault(item => item.Id == id);
    }

    public void CreateSession(Session session)
    {
        ArgumentNullException.ThrowIfNull(session);

        using var transaction = _context.Database.BeginTransaction();

        try
        {
            _context.Sessions.Add(session);
            _context.SaveChanges();
            transaction.Commit();

            _logger.Information(
                "Создана сессия {SessionId}, строк ведомости: {Count}",
                session.Id,
                session.SessionDisciplines.Count);
        }
        catch (Exception exception)
        {
            transaction.Rollback();
            _logger.Error(exception, "Ошибка при создании сессии");
            throw;
        }
    }
}
