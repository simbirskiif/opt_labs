using System.ComponentModel.DataAnnotations;

namespace SessionAccounting.Entities;

public class Faculty
{
    protected Faculty()
    {
    }

    [Key]
    public int Id { get; private set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; private set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    public string ShortName { get; private set; } = string.Empty;

    public static Faculty CreateEntity(int id, string name, string shortName)
    {
        return new Faculty
        {
            Id = id,
            Name = name ?? string.Empty,
            ShortName = shortName ?? string.Empty
        };
    }
}
