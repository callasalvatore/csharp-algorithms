namespace Algorithms.Arrays;

/// <summary>
/// The positions of the two items that add up to the target.
/// </summary>
/// <param name="FirstIndex">The position of the first item in the input list (the smaller of the two positions).</param>
/// <param name="SecondIndex">The position of the second item in the input list.</param>
public record PairResult(int FirstIndex, int SecondIndex);
