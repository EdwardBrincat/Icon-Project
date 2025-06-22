using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Icon_Automation_Libs.Extensions;

public static class StringExtensions
{
    /// <summary>
    /// Determine whether string is null or empty.
    /// </summary>
    /// <param name="value">Value to check.</param>
    /// <returns>Returns true when null or empty.</returns>
    public static bool IsNullOrEmpty([NotNullWhen(false)] this string? value)
        => string.IsNullOrEmpty(value);

    public static bool IsNullOrEmptyOrEqual(this string? value, string expectedValue)
        => value.IsNullOrEmpty() || value == expectedValue;

    /// <summary>
    /// Invokes an action when value is null or empty.
    /// </summary>
    /// <param name="value">Value to check.</param>
    /// <param name="action">Action to invoke when null/empty.</param>
    public static void IfNullOrEmptyThen(this string? value, Action action)
    {
        if (value.IsNullOrEmpty())
            action();
    }

    /// <summary>
    /// Invokes an action when value is not null or empty.
    /// </summary>
    /// <param name="value">Value to check.</param>
    /// <param name="action">Action to invoke when not null/empty.</param>
    public static void IfNotNullOrEmptyThen(this string? value, Action<string> action)
    {
        if (!value.IsNullOrEmpty())
            action(value);
    }

    /// <summary>
    /// Invokes an action and return a new value, when value is null/empty, else return original value.
    /// </summary>
    /// <param name="value">Value to check.</param>
    /// <param name="func">Action to invoke when null/empty which returns a new value.</param>
    /// <returns>Returns new value from func when null/empty or original value.</returns>
    public static string IfNullOrEmptyReturn(this string? value, Func<string> func)
        => value.IsNullOrEmpty() ? func() : value;

    /// <summary>
    /// Returns defaultValue when null/empty, else return original value.
    /// </summary>
    /// <param name="value">Value to check.</param>
    /// <param name="defaultValue">Value to return when null/empty.</param>
    public static string IfNullOrEmptyReturn(this string? value, string defaultValue) => value.IsNullOrEmpty() ? defaultValue : value;

    /// <summary>
	/// Transforms first character of string to upper case and the rest to lower case.
	/// </summary>
	/// <param name="value">Value to transform.</param>
	/// <returns></returns>
	public static string ToUpperFirst(this string value)
        => value[0].ToString().ToUpper() + value[1..].ToLower();

    public static bool ToBool(this string value) => bool.Parse(value);
    
    public static T ToEnum<T>(this string value) => (T)Enum.Parse(typeof(T), value, true);

    public static string ConvertToSnakeCase(this string value, string token = "-")
    {
        var formattedString = string.Empty;

        if (value.Contains(token))
        {
            var values = value.Split(token);

            foreach(var v in values)            
                formattedString += v.ToUpperFirst();
        }
        else
            formattedString = value.ToUpperFirst();

        return formattedString;
    }

	public static string ConvertToUpperSpaced(this string value)
	{
		string spaced = Regex.Replace(value, "([a-z])([A-Z])", "$1 $2");

		TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
		var result = textInfo.ToTitleCase(spaced);

		return result;
	}

	public static string ExtractAmount(this string input)
	{
		try
		{
			// Define the regex pattern to match a dollar sign followed by a number
			var pattern = @"\$(\d+(\.\d{1,2})?)";

			// Match the pattern in the input string
			var match = Regex.Match(input, pattern);

			if (match.Success)
			{
				// Extract and parse the numeric part
				var amountString = match.Groups[0].Value;
				return amountString;
			}

			throw new FormatException("No valid amount found in the input string.");
		}
		catch (Exception ex)
		{
			throw new InvalidOperationException($"Failed to extract amount: {ex.Message}", ex);
		}
	}
	public static string RemoveCurrency(this string value)
		=> Regex.Replace(value, "[^0-9,.]", "", RegexOptions.Compiled);
}
