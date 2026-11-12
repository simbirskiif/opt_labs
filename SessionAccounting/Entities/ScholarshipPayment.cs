using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SessionAccounting.Entities;

public class ScholarshipPayment
{
    protected ScholarshipPayment()
    {
    }

    [Key]
    public int Id { get; private set; }

    [Required]
    public int StudentId { get; private set; }

    public DateTime PaymentDate { get; private set; }

    [Column(TypeName = "numeric(10,2)")]
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
