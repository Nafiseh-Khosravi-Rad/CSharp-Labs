using System;

class Program
{
    static void Main()
    {
        string str = "Vanier College";

        Console.WriteLine("An interactive C# program from " + str);

        const double STATE_TAX = 0.06875;

        Console.Write("Enter a first number: ");
        int num1 = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter a second number: ");
        int num2 = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter a third number: ");
        double num3 = Convert.ToDouble(Console.ReadLine());

        int calc1 = 4 * num1 - 5 * num2;
        double calc2 = 10 * num3;

        Console.WriteLine("calc1 value is: " + calc1);
        Console.WriteLine("calc2 value is: " + calc2);

        Console.WriteLine(
            "calc1 value is " + calc1 +
            " and calc2 value is " + calc2.ToString("F2")
        );

        CalcTax();
    }

    static void CalcTax()
    {
        Console.WriteLine("later calculate taxes");
    }
}