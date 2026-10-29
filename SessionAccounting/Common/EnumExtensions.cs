using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;

namespace SessionAccounting.Common;

public static class EnumExtensions
{
    public static string GetDescription(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = field?.GetCustomAttribute<DescriptionAttribute>();
        return attribute?.Description ?? value.ToString();
    }

    public static void BindEnum<TEnum>(this ComboBox comboBox) where TEnum : struct, Enum
    {
        comboBox.DataSource = Enum.GetValues<TEnum>()
            .Select(value => new { Value = value, Display = value.GetDescription() })
            .ToList();

        comboBox.DisplayMember = "Display";
        comboBox.ValueMember = "Value";
    }
}
