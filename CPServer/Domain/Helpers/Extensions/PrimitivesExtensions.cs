namespace CPServer.Domain;

public static class PrimitivesExtensions
{
    public static string F(this string input, params object?[] args)
    {
        return string.Format(input, args);
    }

    public static bool HasValue(this string input)
    {
        return string.IsNullOrEmpty(input);
    }
}