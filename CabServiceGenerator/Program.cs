using System;

namespace CabInvoiceGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to CabInvoiceGenerator Problem");

            InvoiceGenerator invoice = new InvoiceGenerator(RideType.NORMAL);

            double fare=invoice.calculateFare(2,5);
            Console.WriteLine("Fare :" + fare);

        }
    }
}
