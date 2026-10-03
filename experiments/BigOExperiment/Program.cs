// Lesson 00 experiment: does the time really grow the way Big-O says?
// List.Contains scans the items one by one (O(n)), HashSet.Contains jumps straight
// to the right bucket (O(1) on average). We search 1,000 missing values in each.
//
// Run it in Release mode for meaningful numbers:
//   dotnet run -c Release --project experiments/BigOExperiment

using System.Diagnostics;
using System.Globalization;

const int lookups = 1_000;
int[] sizes = [1_000, 10_000, 100_000];

Console.WriteLine(string.Create(CultureInfo.InvariantCulture, $"Time to search {lookups:N0} missing values:"));
Console.WriteLine();
Console.WriteLine($"{"Items",10} {"List (O(n))",15} {"HashSet (O(1))",16}");

foreach (var size in sizes)
{
    var list = Enumerable.Range(0, size).ToList();
    var set = new HashSet<int>(list);

    var listTime = Measure(() => list.Contains(-1));
    var setTime = Measure(() => set.Contains(-1));

    Console.WriteLine(string.Create(CultureInfo.InvariantCulture,
        $"{size,10:N0} {listTime,12:0.000} ms {setTime,13:0.000} ms"));
}

Console.WriteLine();
Console.WriteLine("List: 10x more items -> about 10x more time. HashSet: the time barely changes.");

static double Measure(Func<bool> search)
{
    // Warm-up: lets the JIT compile the code before we start the stopwatch
    for (var i = 0; i < 100; i++)
        search();

    var stopwatch = Stopwatch.StartNew();
    for (var i = 0; i < lookups; i++)
        search();
    stopwatch.Stop();

    return stopwatch.Elapsed.TotalMilliseconds;
}
