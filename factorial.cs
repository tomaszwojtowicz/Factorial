using System;

// using BigInteger to allow n! for n > 170
using BigInt = System.Numerics.BigInteger;

class Factorial
{
    ///<summary>
    ///Calculates factorial of a natural number
    ///</summary>
    public static BigInt FactorialBinarySplit(uint n)
    {
        uint low = 1;
        uint high;
        uint s = 0;
        BigInt P = 1;
        BigInt Q = 1;

        int starting_i = FloorBinaryLogarithm((int)(n / 2));

        for (int i = starting_i; i >= 0; i--)
        {
            high = n >> i;
            s += high / 2;
            high = high - 1 | 1;
            P *= Q;
            Q *= OddProduct(high, low);
            low = high + 2;
        }

        return Q * P << (int)s;
    }

    ///<summary>
    ///Calculates product of range of natural odd numbers
    ///<para>Expects arguments in descending order</para>
    ///</summary>
    private static BigInt OddProduct(uint high, uint low)
    {
        // throw exception if inputs are not odd
        if ((high % 2 == 0) || (low % 2 == 0))
        {
            throw new ArgumentException("Incorrect input: Both arguments are required to be odd");
        }

        if (low == high) return high;

        // a precaution in case input values were in the wrong order
        else if (low > high)
        {
            uint tmp = low;
            low = high;
            high = tmp;
        }

        uint middle = (low + high) / 2;

        // if odd, make the middle even
        middle += middle & 1;

        return OddProduct(high, middle + 1) * OddProduct(middle - 1, low);
    }

    ///<summary>
    ///Calculates the floor of binary logarithm
    ///</summary>
    private static int FloorBinaryLogarithm(int n)
    {
        int result = -1;

        for (; n > 0; n /= 2)

        {
            result++;
        }

        return result;
    }

    static void Main()
    {
        Console.WriteLine("\n" + FactorialBinarySplit(999));
    }
}