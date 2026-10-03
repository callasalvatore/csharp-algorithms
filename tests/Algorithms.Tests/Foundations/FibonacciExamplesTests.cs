using Algorithms.Foundations;

namespace Algorithms.Tests.Foundations;

public class FibonacciExamplesTests
{
    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    [InlineData(2, 1)]
    [InlineData(10, 55)]
    [InlineData(20, 6765)]
    public void BothVersions_ComputeTheSameValue(int n, long expected)
    {
        Assert.Equal(expected, FibonacciExamples.Recursive(n).Value);
        Assert.Equal(expected, FibonacciExamples.Iterative(n).Value);
    }

    [Theory]
    [InlineData(10, 177)]
    [InlineData(20, 21_891)]
    [InlineData(30, 2_692_537)]
    public void Recursive_CallsGrowExponentially(int n, long expectedCalls)
    {
        // +10 on n multiplies the calls by more than 100 → O(2ⁿ)
        Assert.Equal(expectedCalls, FibonacciExamples.Recursive(n).Calls);
    }

    [Theory]
    [InlineData(10, 9)]
    [InlineData(30, 29)]
    public void Iterative_StepsGrowLinearly(int n, int expectedSteps)
    {
        Assert.Equal(expectedSteps, FibonacciExamples.Iterative(n).Steps);
    }
}
