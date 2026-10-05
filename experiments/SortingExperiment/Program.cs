// Appendix E experiment: how many comparisons do insertion sort and merge sort make
// on random, already sorted and reversed input?
//
//   dotnet run -c Release --project experiments/SortingExperiment

using System.Globalization;
using Algorithms.Appendix;

CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

int[] sizes = [100, 1_000, 10_000];
var random = new Random(42);

Console.WriteLine("Comparisons on RANDOM input:");
Console.WriteLine($"{"n",8} {"Insertion sort",16} {"Merge sort",12}");
foreach (var n in sizes)
{
    var items = Enumerable.Range(0, n).Select(_ => random.Next()).ToArray();
    Console.WriteLine($"{n,8:N0} {SortingExamples.InsertionSort(items).Comparisons,16:N0} {SortingExamples.MergeSort(items).Comparisons,12:N0}");
}

Console.WriteLine();
Console.WriteLine("Insertion sort, n = 10,000, depending on the input order:");
var sorted = Enumerable.Range(0, 10_000).ToArray();
var reversed = sorted.Reverse().ToArray();
Console.WriteLine($"  already sorted: {SortingExamples.InsertionSort(sorted).Comparisons,12:N0}");
Console.WriteLine($"  reversed:       {SortingExamples.InsertionSort(reversed).Comparisons,12:N0}");
