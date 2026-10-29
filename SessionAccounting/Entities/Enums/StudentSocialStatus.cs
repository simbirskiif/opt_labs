using System.ComponentModel;

namespace SessionAccounting.Entities.Enums;

[Flags]
public enum StudentSocialStatus
{
    [Description("Нет")]
    None = 0,

    [Description("Семейный")]
    Married = 1,

    [Description("Общежитие")]
    Dormitory = 2
}
