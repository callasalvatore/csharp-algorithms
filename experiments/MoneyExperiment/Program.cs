// Appendix A experiment: what happens to money stored as double, decimal and integer cents.
//
//   dotnet run --project experiments/MoneyExperiment

using System.Globalization;
using Algorithms.Appendix;
using Algorithms.Arrays;

CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

Console.WriteLine("1. A simple sum");
Console.WriteLine($"   double:  0.1 + 0.2 = {0.1 + 0.2:R}   equals 0.3? {0.1 + 0.2 == 0.3}");
Console.WriteLine($"   decimal: 0.1 + 0.2 = {0.1m + 0.2m}                 equals 0.3? {0.1m + 0.2m == 0.3m}");
Console.WriteLine();

Console.WriteLine("2. Adding 0.10 EUR ten times");
double doubleTotal = 0;
decimal decimalTotal = 0;
long centsTotal = 0;
for (var i = 0; i < 10; i++)
{
    doubleTotal += 0.10;
    decimalTotal += 0.10m;
    centsTotal += 10;
}
Console.WriteLine($"   double:  {doubleTotal:R}   equals 1.00? {doubleTotal == 1.0}");
Console.WriteLine($"   decimal: {decimalTotal}                 equals 1.00? {decimalTotal == 1.00m}");
Console.WriteLine($"   cents:   {centsTotal}                   equals 100?  {centsTotal == 100}");
Console.WriteLine();

Console.WriteLine("3. Two Sum: which two invoices does a 1,250.30 EUR transfer pay?");
Console.WriteLine("   Invoices: 310.00, 475.00, 820.10, 155.00, 430.20 (the answer is 820.10 + 430.20)");
Console.WriteLine($"   double:  {FindPairWithDoubles([310.00, 475.00, 820.10, 155.00, 430.20], 1250.30)}");
Console.WriteLine($"   decimal: {FindPairWithDecimals([310.00m, 475.00m, 820.10m, 155.00m, 430.20m], 1250.30m)}");
var centsPair = TwoSum.WithDictionary([31_000, 47_500, 82_010, 15_500, 43_020], 125_030).Pair;
Console.WriteLine($"   cents:   {(centsPair is null ? "no pair found" : $"found positions {centsPair.FirstIndex} and {centsPair.SecondIndex}")}");
Console.WriteLine($"   (with doubles, 1250.30 - 430.20 = {1250.30 - 430.20:R}, not 820.10)");
Console.WriteLine();

Console.WriteLine("4. Rounding half-way values");
Console.WriteLine($"   Math.Round(2.5m) = {Math.Round(2.5m)}   Math.Round(3.5m) = {Math.Round(3.5m)}   (to even: the default)");
Console.WriteLine($"   Math.Round(2.5m, MidpointRounding.AwayFromZero) = {Math.Round(2.5m, MidpointRounding.AwayFromZero)}");
Console.WriteLine();

Console.WriteLine("5. Splitting 100.00 EUR into 3 installments");
var naivePart = Math.Round(100.00m / 3, 2);
Console.WriteLine($"   naive:  3 x {naivePart} = {naivePart * 3}   (one cent lost)");
var parts = MoneyAllocation.Split(10_000, 3);
Console.WriteLine($"   Split:  {string.Join(" + ", parts.Select(p => (p / 100m).ToString("0.00")))} = {parts.Sum() / 100m:0.00}");

// Two Sum with a dictionary, as in lesson 01, but with double keys
static string FindPairWithDoubles(double[] amounts, double target)
{
    var seen = new Dictionary<double, int>();
    for (var i = 0; i < amounts.Length; i++)
    {
        if (seen.TryGetValue(target - amounts[i], out var j))
            return $"found positions {j} and {i}";
        seen.TryAdd(amounts[i], i);
    }
    return "no pair found  <-- wrong!";
}

// The same, with decimal keys
static string FindPairWithDecimals(decimal[] amounts, decimal target)
{
    var seen = new Dictionary<decimal, int>();
    for (var i = 0; i < amounts.Length; i++)
    {
        if (seen.TryGetValue(target - amounts[i], out var j))
            return $"found positions {j} and {i}";
        seen.TryAdd(amounts[i], i);
    }
    return "no pair found";
}
