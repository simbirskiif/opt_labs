namespace SessionAccounting.Entities;

public class ScholarshipPayment
{
    public int Id { get; private set; }

    public int StudentId { get; private set; }

    public DateTime PaymentDate { get; private set; }

    public decimal Amount { get; private set; }

    public static ScholarshipPayment CreateOperation(int id, int studentId, decimal amount)
    {
        return new ScholarshipPayment
        {
            Id = id,
            StudentId = studentId,
            PaymentDate = DateTime.Now,
            Amount = amount
        };
    }
}
