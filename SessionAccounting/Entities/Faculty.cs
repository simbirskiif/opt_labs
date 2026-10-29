namespace SessionAccounting.Entities;

public class Faculty
{
    public int Id { get; private set; }

    public string Name { get; private set; } = string.Empty;

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
