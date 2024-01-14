using System.Globalization;

namespace Models;

public static class CultureDefaults
{
    public const string CultureFR = "fr-FR";
    public static readonly List<CultureInfo> SupportedCultures = new() { new CultureInfo(CultureFR), new CultureInfo("en-US") };
}
