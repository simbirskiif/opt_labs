using System.ComponentModel;
using SessionAccounting.Entities.Enums;

namespace SessionAccounting.Entities;

public class Student
{
    public int Id { get; private set; }

    public int GroupId { get; private set; }

    public string RecordBookNumber { get; private set; } = string.Empty;

    public string LastName { get; private set; } = string.Empty;

    public string FirstName { get; private set; } = string.Empty;

    public string MiddleName { get; private set; } = string.Empty;

    public StudentSocialStatus SocialStatus { get; private set; }

    public decimal AverageGrade { get; private set; }

    public decimal ScholarshipAmount { get; private set; }

    public decimal DormitoryPayment { get; private set; }

    [Browsable(false)]
    public string FullName => $"{LastName} {FirstName} {MiddleName}".Trim();

    public static Student CreateEntity(
        int id,
        int groupId,
        string recordBookNumber,
        string lastName,
        string firstName,
        string middleName,
        StudentSocialStatus socialStatus,
        decimal averageGrade)
    {
        var scholarshipAmount = CalculateScholarship(averageGrade, socialStatus);
        var dormitoryPayment = socialStatus.HasFlag(StudentSocialStatus.Dormitory) && scholarshipAmount > 0
            ? 100m
            : 0m;

        return new Student
        {
            Id = id,
            GroupId = groupId,
            RecordBookNumber = recordBookNumber ?? string.Empty,
            LastName = lastName ?? string.Empty,
            FirstName = firstName ?? string.Empty,
            MiddleName = middleName ?? string.Empty,
            SocialStatus = socialStatus,
            AverageGrade = averageGrade,
            ScholarshipAmount = scholarshipAmount,
            DormitoryPayment = dormitoryPayment
        };
    }

    public static decimal CalculateScholarship(decimal averageGrade, StudentSocialStatus socialStatus)
    {
        if (averageGrade < 4.0m)
        {
            return 0m;
        }

        var amount = averageGrade >= 5.0m
            ? 1200m
            : averageGrade >= 4.5m
                ? 900m
                : 600m;

        if (socialStatus.HasFlag(StudentSocialStatus.Married))
        {
            amount += 600m;
        }

        if (socialStatus.HasFlag(StudentSocialStatus.Dormitory))
        {
            amount -= 100m;
        }

        return Math.Max(0m, amount);
    }
}
