namespace CPServer.Domain.Helpers.Extensions;

public static class EnumerableExtensions
{
    public static string ToDelimiterSeparatedValues(
        this IEnumerable<string> values,
        string separator = ",",
        bool addSpaceAfterSeparator = true)
    {
        if (values == null)
        {
            return string.Empty;
        }

        if (addSpaceAfterSeparator)
        {
            separator += " ";
        }

        var result = string.Join(separator, values);

        return result;
    }
    
}
