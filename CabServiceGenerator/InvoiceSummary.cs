using System;
using System.Collections.Generic;
using System.Text;

namespace CabInvoiceGenerator
{
   public class InvoiceSummary
    {
        public int numberOfRides;
         public double totalFare;
        public double avgFare;

        public InvoiceSummary(int numberOfRides,double totalFare)

        {
            this.numberOfRides = numberOfRides;
            this.totalFare = totalFare;
            this.avgFare = this.totalFare / this.numberOfRides;
        }

        public override bool Equals(object obj)
        {
            if(obj==null)
            {
                return false;
            }
            if(!(obj is InvoiceSummary))
            {
                return false;
            }

            InvoiceSummary inputObj = (InvoiceSummary)obj;
            return this.numberOfRides == inputObj.numberOfRides && this.totalFare == inputObj.totalFare && this.avgFare == inputObj.avgFare;
        }

        public override int GetHashCode()
        {
            return this.numberOfRides.GetHashCode() ^ this.totalFare.GetHashCode() ^ this.avgFare.GetHashCode();

        }
    }
}
