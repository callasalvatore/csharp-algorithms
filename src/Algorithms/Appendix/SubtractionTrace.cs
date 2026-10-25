using System.Numerics;

namespace Algorithms.Appendix;

/// <summary>
/// Appendix B: the steps the processor follows to subtract two doubles, written out in binary.
/// The bits are computed exactly, so they match what happens inside the hardware before the final rounding.
/// </summary>
/// <param name="MinuendAligned">The first number, with the shared exponent, e.g. <c>1.0011…00</c>.</param>
/// <param name="SubtrahendAligned">The second number, moved to the shared exponent, e.g. <c>0.0110…11</c>.</param>
/// <param name="ExactDifference">The exact difference of the two aligned numbers, before normalizing.</param>
/// <param name="AlignedExponent">The shared exponent used for the three lines above (the exponent of the first number).</param>
/// <param name="Normalized">The exact difference written as <c>1.xxx</c>, with every bit after the point.</param>
/// <param name="NormalizedExponent">The exponent after normalizing.</param>
/// <param name="FittingFraction">The 52 bits after the point that fit in a double.</param>
/// <param name="LeftoverBits">The bits after the 52nd that don't fit and must be rounded away.</param>
public record SubtractionTrace(
    string MinuendAligned,
    string SubtrahendAligned,
    string ExactDifference,
    int AlignedExponent,
    string Normalized,
    int NormalizedExponent,
    string FittingFraction,
    string LeftoverBits)
{
    private const int FractionBits = 52;

    /// <summary>
    /// Writes out the subtraction <paramref name="minuend"/> - <paramref name="subtrahend"/> step by step.
    /// </summary>
    /// <param name="minuend">The number to subtract from, e.g. 1250.30. Must be a normal, positive double.</param>
    /// <param name="subtrahend">
    /// The number to subtract, e.g. 430.20. Must be a normal, positive double, smaller than <paramref name="minuend"/>.
    /// </param>
    /// <returns>The aligned numbers, the exact difference, the normalized result and the bits that don't fit.</returns>
    /// <exception cref="ArgumentOutOfRangeException">The numbers aren't both positive, or the subtrahend isn't smaller.</exception>
    public static SubtractionTrace Of(double minuend, double subtrahend)
    {
        if (!(subtrahend > 0 && minuend > subtrahend) || !double.IsNormal(minuend) || !double.IsNormal(subtrahend))
            throw new ArgumentOutOfRangeException(nameof(subtrahend), "Both numbers must be positive, and the subtrahend smaller.");

        var (significandA, exponentA) = Split(minuend);
        var (significandB, exponentB) = Split(subtrahend);
        var shift = exponentA - exponentB;

        // Both numbers as whole multiples of the smallest unit involved: 2^(exponentB - 52)
        var a = significandA << shift;
        var b = significandB;
        var difference = a - b;

        // Every aligned line has 1 digit before the point and FractionBits + shift after it
        var alignedWidth = FractionBits + shift + 1;
        var differenceBits = (int)difference.GetBitLength();
        var normalizedFraction = ToBinary(difference, differenceBits)[1..];

        return new SubtractionTrace(
            WithPoint(ToBinary(a, alignedWidth)),
            WithPoint(ToBinary(b, alignedWidth)),
            WithPoint(ToBinary(difference, alignedWidth)),
            exponentA,
            "1." + normalizedFraction,
            exponentA - (alignedWidth - differenceBits),
            normalizedFraction[..FractionBits],
            normalizedFraction[FractionBits..]);
    }

    private static (BigInteger Significand, int Exponent) Split(double value)
    {
        long bits = BitConverter.DoubleToInt64Bits(value);
        var exponent = (int)((bits >> FractionBits) & 0x7FF) - 1023;
        BigInteger significand = (bits & 0xF_FFFF_FFFF_FFFF) | (1L << FractionBits);   // the hidden 1 added back
        return (significand, exponent);
    }

    private static string ToBinary(BigInteger value, int width)
    {
        var digits = new char[width];
        for (var i = 0; i < width; i++)
            digits[width - 1 - i] = ((value >> i) & 1) == 1 ? '1' : '0';
        return new string(digits);
    }

    private static string WithPoint(string bits) => bits[..1] + "." + bits[1..];
}
