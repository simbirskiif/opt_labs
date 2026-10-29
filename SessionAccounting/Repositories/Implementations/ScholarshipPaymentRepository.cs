using SessionAccounting.Entities;

namespace SessionAccounting.Repositories.Implementations;

public sealed class ScholarshipPaymentRepository : IScholarshipPaymentRepository
{
    public IEnumerable<ScholarshipPayment> ReadScholarshipPayments(
        DateTime? dateFrom = null,
        DateTime? dateTo = null,
        int? studentId = null)
    {
        return [];
    }

    public void CreateScholarshipPayment(ScholarshipPayment payment)
    {
    }

    public void DeleteScholarshipPayment(int id)
    {
    }
}
