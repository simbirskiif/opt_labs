using SessionAccounting.Entities;

namespace SessionAccounting.Repositories;

public interface ISessionRepository
{
    IEnumerable<Session> ReadSessions(
        DateTime? dateFrom = null,
        DateTime? dateTo = null,
        int? studentId = null,
        int? groupId = null);

    Session? ReadSessionById(int id);

    void CreateSession(Session session);
}
