// Appendix B experiment: reads the 8 bytes of a double directly from memory,
// and shows where doubles live: on the stack or on the heap.
//
//   dotnet run --project experiments/DoubleInMemory

unsafe
{
    // Section 1: the 8 bytes of a double
    double amount = 1250.30;
    byte* address = (byte*)&amount;     // &amount: the address of the variable

    Console.WriteLine($"1250.30 as one 64-bit number: 0x{BitConverter.DoubleToInt64Bits(amount):X16}");
    Console.WriteLine($"BitConverter.IsLittleEndian = {BitConverter.IsLittleEndian}");
    Console.WriteLine();
    Console.WriteLine("Address              Byte");

    for (var i = 0; i < 8; i++)
        Console.WriteLine($"0x{(ulong)(address + i):X16}   {address[i]:X2}");

    Console.WriteLine();
    Console.WriteLine($"BitConverter.GetBytes(1250.30): {BitConverter.ToString(BitConverter.GetBytes(amount))}");

    // Section 2: stack and heap
    double other = 430.20;
    double[] amounts = [1250.30, 430.20];
    var invoice = new Invoice { Amount = 1250.30 };

    Console.WriteLine();
    Console.WriteLine($"a local variable:        0x{(ulong)&amount:X16}");
    Console.WriteLine($"another local variable:  0x{(ulong)&other:X16}");

    // fixed: tells the garbage collector not to move the array while we read its address
    fixed (double* first = amounts)
    {
        Console.WriteLine($"amounts[0]:              0x{(ulong)first:X16}");
        Console.WriteLine($"amounts[1]:              0x{(ulong)(first + 1):X16}");
    }

    fixed (double* field = &invoice.Amount)
        Console.WriteLine($"invoice.Amount:          0x{(ulong)field:X16}");
}

internal class Invoice
{
    public double Amount;
}
