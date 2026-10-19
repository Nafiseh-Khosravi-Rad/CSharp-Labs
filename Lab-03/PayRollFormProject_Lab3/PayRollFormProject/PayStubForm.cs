using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PayRollFormProject
{
    public partial class PayStubForm : Form
    {
        public PayStubForm(
     string employeeName,
     string ssn,
     double totalIncome,
     double fedTax,
     double prvTax,
     double qpIns,
     double eIns,
     double qpp,
     double unionDues,
     double totalDeductions,
     double netAmount)
        {
            InitializeComponent();

            lblPayStubDetails.Text =
                "Employee Name: " + employeeName +
                "\nSSN Number: " + ssn +
                "\nTotal Earning is $" + totalIncome.ToString("N2") +
                "\nThe Fed_Tax Deduction is $" + fedTax.ToString("N2") +
                "\nThe Prv_Tax Deduction is $" + prvTax.ToString("N2") +
                "\nThe QP_Ins Deduction is $" + qpIns.ToString("N2") +
                "\nThe E_Ins Deduction is $" + eIns.ToString("N2") +
                "\nThe Qpp Deduction is $" + qpp.ToString("N2") +
                "\nThe Union_D Deduction is $" + unionDues.ToString("N2") +
                "\nThe Total Deduction is $" + totalDeductions.ToString("N2") +
                "\n\nThe Total Net Amount is $" + netAmount.ToString("N2");
        }
    }
}
