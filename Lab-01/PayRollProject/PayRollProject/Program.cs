using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("The Pay Stub System");
        Console.WriteLine();

        Console.Write("Enter Employee's Name: ");
        string employeeName = Console.ReadLine();

        Console.Write("Enter Employee's SSN: ");
        string ssn = Console.ReadLine();

        Console.Write("Enter Number of Worked Hours: ");
        double numberWhr = Convert.ToDouble(Console.ReadLine());

        Console.Write("Enter Hour Rate: ");
        double hRate = Convert.ToDouble(Console.ReadLine());

        double totalIncome = numberWhr * hRate;

        double prvTax = totalIncome * 0.09;
        double fedTax = totalIncome * 0.07;
        double qpIns = totalIncome * 0.0055;
        double eIns = totalIncome * 0.014;
        double qpp = totalIncome * 0.045;
        double unionD = totalIncome * 0.0165;

        double totalDeductions =
            prvTax + fedTax + qpIns + eIns + qpp + unionD;

        double netAmount = totalIncome - totalDeductions;

        Console.WriteLine();
        Console.WriteLine("----- PAY STUB -----");
        Console.WriteLine("Employee Name: " + employeeName);
        Console.WriteLine("SSN: " + ssn);
        Console.WriteLine("Total Income: $" + totalIncome.ToString("F2"));
        Console.WriteLine("Federal Tax Deduction: $" + fedTax.ToString("F2"));
        Console.WriteLine("Provincial Tax Deduction: $" + prvTax.ToString("F2"));
        Console.WriteLine("QP Insurance Deduction: $" + qpIns.ToString("F2"));
        Console.WriteLine("Employment Insurance Deduction: $" + eIns.ToString("F2"));
        Console.WriteLine("QPP Deduction: $" + qpp.ToString("F2"));
        Console.WriteLine("Union Deduction: $" + unionD.ToString("F2"));
        Console.WriteLine("Total Deduction: $" + totalDeductions.ToString("F2"));
        Console.WriteLine("Net Amount: $" + netAmount.ToString("F2"));
    }
}