using System.Text.RegularExpressions;

namespace MyArabic.WebApi.Validators;

public partial class SlugValidator
{
    private static readonly Regex SlugRegex = MyRegex();

    public static bool Validate(string slug)
    {
        return !string.IsNullOrWhiteSpace(slug) && SlugRegex.IsMatch(slug);
    }

    [GeneratedRegex(@"^[a-z0-9]+(-[a-z0-9]+)*$", RegexOptions.Compiled)]
    private static partial Regex MyRegex();
}
