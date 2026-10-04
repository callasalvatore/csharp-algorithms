namespace Algorithms.Appendix;

/// <summary>
/// Appendix A: splitting an amount of money into equal parts without losing or creating cents.
/// </summary>
public static class MoneyAllocation
{
    /// <summary>
    /// Splits <paramref name="totalInMinorUnits"/> into <paramref name="parts"/> amounts that differ by at most
    /// one minor unit and always add up exactly to the total. The leftover units go to the first parts:
    /// 100.00 EUR in 3 parts becomes 33.34 + 33.33 + 33.33.
    /// Time O(parts), space O(parts) for the result.
    /// </summary>
    /// <param name="totalInMinorUnits">
    /// The amount to split, in the smallest unit of its currency (cents for EUR: 10000 = 100.00 EUR).
    /// It can be negative, e.g. a refund to split across installments.
    /// </param>
    /// <param name="parts">How many parts to create, e.g. the number of installments. Must be at least 1.</param>
    /// <returns>
    /// The amounts of the parts, in minor units, in order. Their sum is exactly <paramref name="totalInMinorUnits"/>.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="parts"/> is less than 1.</exception>
    public static long[] Split(long totalInMinorUnits, int parts)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(parts, 1);

        // 10000 / 3 = 3333 with remainder 1: every part gets 3333, one part gets 1 more
        var (quotient, remainder) = Math.DivRem(totalInMinorUnits, parts);

        // With a negative total the remainder is negative too: the extra units are -1 each
        var extra = Math.Sign(remainder);

        var result = new long[parts];
        for (var i = 0; i < parts; i++)
            result[i] = quotient + (i < Math.Abs(remainder) ? extra : 0);

        return result;
    }
}
