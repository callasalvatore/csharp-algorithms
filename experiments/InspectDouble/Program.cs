// Appendix A experiment: shows how a double is really stored.
//
//   dotnet run --project experiments/InspectDouble                 (the numbers of the appendix)
//   dotnet run --project experiments/InspectDouble -- 0.3 2.75     (any numbers you like)

using System.Globalization;
using Algorithms.Appendix;

CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

var numbers = args.Length > 0
    ? args.Select(arg => double.Parse(arg, CultureInfo.InvariantCulture)).ToArray()
    : [0.1, 1250.30, 430.20, 820.10];

foreach (var number in numbers)
{
    Console.WriteLine($"You wrote:   {number}");
    Console.WriteLine($"  bits:      {DoubleInspector.Bits(number)}");
    Console.WriteLine("             s exponent    fraction (52 bits)");
    Console.WriteLine($"  hex:       0x{BitConverter.DoubleToInt64Bits(number):X16}");
    Console.WriteLine($"  exponent:  {DoubleInspector.Exponent(number)}");
    Console.WriteLine($"  stored:    {DoubleInspector.ExactValue(number)}");
    Console.WriteLine($"  next gap:  {DoubleInspector.GapToNext(number):G6}");
    Console.WriteLine();
}
