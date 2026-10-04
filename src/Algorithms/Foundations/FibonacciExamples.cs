namespace Algorithms.Foundations;

/// <summary>
/// Lesson 00: the classic example of exponential growth, O(2ⁿ),
/// and how the same result can be computed in O(n).
/// </summary>
public static class FibonacciExamples
{
    /// <summary>
    /// The textbook recursive definition: fib(n) = fib(n - 1) + fib(n - 2).
    /// Every call makes two more calls, so the work roughly doubles at each n → O(2ⁿ).
    /// </summary>
    /// <param name="n">
    /// The position in the sequence, starting from fib(0) = 0, fib(1) = 1.
    /// Keep it small (≤ 35): the number of calls explodes.
    /// </param>
    /// <returns>
    /// <c>Value</c>: the n-th Fibonacci number.
    /// <c>Calls</c>: how many times the recursive function was called.
    /// </returns>
    public static (long Value, long Calls) Recursive(int n)
    {
        long calls = 0;

        long Fib(int k)
        {
            calls++;
            return k < 2 ? k : Fib(k - 1) + Fib(k - 2);
        }

        return (Fib(n), calls);
    }

    /// <summary>
    /// Walks forward keeping only the last two values. Time O(n), space O(1).
    /// </summary>
    /// <param name="n">
    /// The position in the sequence, starting from fib(0) = 0, fib(1) = 1.
    /// Up to 92: fib(93) doesn't fit in a <c>long</c>.
    /// </param>
    /// <returns>
    /// <c>Value</c>: the n-th Fibonacci number.
    /// <c>Steps</c>: how many loop iterations were needed (n - 1 for n ≥ 2).
    /// </returns>
    public static (long Value, int Steps) Iterative(int n)
    {
        if (n < 2)
            return (n, 0);

        long previous = 0;
        long current = 1;
        var steps = 0;

        for (var k = 2; k <= n; k++)
        {
            steps++;
            (previous, current) = (current, previous + current);
        }

        return (current, steps);
    }
}
