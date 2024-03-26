namespace CPServer.Domain;

public static class EnumExtensions
{
    public static string ToEnumStringDisplayValue<TEnum>(this TEnum value) 
        where TEnum : struct
    {
        return value.GetType().ToString();
    }
}