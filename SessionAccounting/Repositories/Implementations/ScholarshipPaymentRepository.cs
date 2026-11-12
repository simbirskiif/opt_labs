using Microsoft.EntityFrameworkCore;
using Serilog;
using SessionAccounting.Data;
using SessionAccounting.Entities;

namespace SessionAccounting.Repositories.Implementations;

public sealed class ScholarshipPaymentRepository : IScholarshipPaymentRepository
{
    private readonly DataContext _context;
    private readonly ILogger _logger;

    public ScholarshipPaymentRepository(DataContext context, ILogger logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public IEnumerable<ScholarshipPayment> ReadScholarshipPayments(
        DateTime? dateFrom = null,
        DateTime? dateTo = null,
        int? studentId = null)
    {
        _logger.Information(
            "Чтение выплат: с {DateFrom}, по {DateTo}, студент {StudentId}",
            dateFrom,
            dateTo,
            studentId);

        var query = _context.ScholarshipPayments.AsNoTracking().AsQueryable();

        if (dateFrom.HasValue)
        {
            query = query.Where(item => item.PaymentDate >= dateFrom.Value);
        }

        if (dateTo.HasValue)
        {
            query = query.Where(item => item.PaymentDate <= dateTo.Value);
        }

        if (studentId.HasValue)
        {
            query = query.Where(item => item.StudentId == studentId.Value);
        }

        return query.OrderByDescending(item => item.PaymentDate).ToList();
    }

    public void CreateScholarshipPayment(ScholarshipPayment payment)
    {
        ArgumentNullException.ThrowIfNull(payment);
        _context.ScholarshipPayments.Add(payment);
        _context.SaveChanges();
        _logger.Information(
            "Создана выплата {PaymentId}: студент {StudentId}, сумма {Amount}",
            payment.Id,
            payment.StudentId,
            payment.Amount);
    }

    public void DeleteScholarshipPayment(int id)
    {
        var payment = _context.ScholarshipPayments.Find(id)
            ?? throw new InvalidOperationException("Выплата не найдена");
        _context.ScholarshipPayments.Remove(payment);
        _context.SaveChanges();
        _logger.Information("Удалена выплата {PaymentId}", id);
    }
}
