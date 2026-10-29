using System.ComponentModel;

namespace SessionAccounting.Entities.Enums;

public enum TeacherPosition
{
    [Description("Не выбрано")]
    None = 0,

    [Description("Ассистент")]
    Assistant = 1,

    [Description("Старший преподаватель")]
    SeniorLecturer = 2,

    [Description("Доцент")]
    Docent = 3,

    [Description("Профессор")]
    Professor = 4
}
