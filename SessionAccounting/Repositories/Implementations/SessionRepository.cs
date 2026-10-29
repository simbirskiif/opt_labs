using SessionAccounting.Entities;

namespace SessionAccounting.Repositories.Implementations;

public sealed class SessionRepository : ISessionRepository
{
    public IEnumerable<Session> ReadSessions(
        DateTime? dateFrom = null,
        DateTime? dateTo = null,
        int? studentId = null,
        int? groupId = null)
    {
        return [];
    }

    public Session? ReadSessionById(int id)
    {
        return null;
    }

    public void CreateSession(Session session)
    {
    }
}
