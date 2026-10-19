using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Create program with two functions called main() and convert_temp()");
        ConvertTemp();
    }

    static void ConvertTemp()
    {
        Console.Write("Enter temperature in Fahrenheit: ");
        double tempFar = Convert.ToDouble(Console.ReadLine());

        double tempCel = (tempFar - 32) * 5.0 / 9.0;

        Console.WriteLine("Temperature in Celsius: " + tempCel.ToString("F2"));
    }
}