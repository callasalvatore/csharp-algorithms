using System.Numerics;

namespace Algorithms.Appendix;

/// <summary>
/// Appendix A: looks inside a <c>double</c> to show how it is really stored.
/// A double is 64 bits: 1 sign bit, 11 exponent bits and 52 fraction bits (IEEE 754).
/// </summary>
public static class DoubleInspector
{
    /// <summary>
    /// The 64 bits of the value, split into its three parts and separated by spaces.
    /// </summary>
    /// <param name="value">The number to inspect, e.g. 1250.30.</param>
    /// <returns>
    /// A string like <c>"0 10000001001 0011100010…"</c>: the sign bit, the 11 exponent bits and the 52 fraction bits.
    /// </returns>
    public static string Bits(double value)
    {
        var bits = Convert.ToString(BitConverter.DoubleToInt64Bits(value), 2).PadLeft(64, '0');
        return $"{bits[0]} {bits[1..12]} {bits[12..]}";
    }

    /// <summary>
    /// The power of 2 the value is multiplied by, once the stored exponent has its bias of 1023 removed.
    /// For example 1250.30 = 1.22… × 2¹⁰, so the result is 10.
    /// </summary>
    /// <param name="value">A normal, non-zero, finite number.</param>
    /// <returns>The exponent, between -1022 and 1023.</returns>
    public static int Exponent(double value)
    {
        var storedExponent = (int)((BitConverter.DoubleToInt64Bits(value) >> 52) & 0x7FF);
        return storedExponent - 1023;
    }

    /// <summary>
    /// The exact decimal value the double holds, with every digit: no rounding at all.
    /// For example, writing <c>0.1</c> in C# stores 0.1000000000000000055511151231257827021181583404541015625.
    /// </summary>
    /// <param name="value">A finite number.</param>
    /// <returns>The exact value written in base 10, with as many digits as needed.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="value"/> is infinite or NaN.</exception>
    public static string ExactValue(double value)
    {
        if (!double.IsFinite(value))
            throw new ArgumentOutOfRangeException(nameof(value), "Only finite numbers have an exact decimal value.");

        // A double is exactly significand × 2^power, with an integer significand
        long bits = BitConverter.DoubleToInt64Bits(value);
        var storedExponent = (int)((bits >> 52) & 0x7FF);
        long fraction = bits & 0xF_FFFF_FFFF_FFFF;

        BigInteger significand = storedExponent == 0 ? fraction : fraction | (1L << 52);
        var power = (storedExponent == 0 ? 1 : storedExponent) - 1075;

        if (power >= 0)
            return (bits < 0 ? "-" : "") + (significand << power);

        // significand / 2^k has exactly k decimal digits: it equals significand × 5^k / 10^k
        var k = -power;
        var digits = (significand * BigInteger.Pow(5, k)).ToString().PadLeft(k + 1, '0');
        var integerPart = digits[..^k];
        var decimalPart = digits[^k..].TrimEnd('0');

        return (bits < 0 ? "-" : "") + integerPart + (decimalPart.Length > 0 ? "." + decimalPart : "");
    }

    /// <summary>
    /// The distance between the value and the next larger double: no double exists in between.
    /// It's called the ULP (unit in the last place) and grows with the size of the number.
    /// </summary>
    /// <param name="value">A finite number.</param>
    /// <returns>The gap to the next double, e.g. 2⁻⁴² (about 2.27e-13) for numbers between 1024 and 2048.</returns>
    public static double GapToNext(double value) => Math.BitIncrement(value) - value;
}
