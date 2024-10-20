using Shared.Enums;
using System.Globalization;

namespace Shared.Models;

public class Product
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = null!;
    public decimal? Price { get; set; }
    public string DisplayProduct => $"{Name}, {Price?.ToString("0.00", CultureInfo.CurrentCulture)} kr"; // Hjälp från ChatGPT för att skriva ut priset med två decimaler

    public Category? Category { get; set; } = new();
}
