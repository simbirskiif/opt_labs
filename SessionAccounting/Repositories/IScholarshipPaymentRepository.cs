using SessionAccounting.Entities;

namespace SessionAccounting.Repositories;

public interface IScholarshipPaymentRepository
{
    IEnumerable<ScholarshipPayment> ReadScholarshipPayments(
        DateTime? dateFrom = null,
        DateTime? dateTo = null,
        int? studentId = null);

    void CreateScholarshipPayment(ScholarshipPayment payment);

    void DeleteScholarshipPayment(int id);
}
