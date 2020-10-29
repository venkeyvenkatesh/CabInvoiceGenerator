using CabInvoiceGenerator;
using NUnit.Framework;

namespace NUnitTestProject1
{
    public class Tests
    {
        //UC1
        //Given distance and time return the total fare
        [Test]
        public void GivenDistanceAndTime_ShouldReturnTotalFare()
        {
            double expected = 25;
            InvoiceGenerator invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);

            double distance = 2.0;
            int time = 5;
            double actual = invoiceGenerator.calculateFare(distance,time);
            Assert.AreEqual(expected, actual);
        }


        //Given the Invalid ride type throw Invalid ride type exception
        [Test]
        public void GivenInvalidRideType_ShouldThrowException()
        {

            string expected = "Invalid ride type";
            string actual = null;
            try
            {
                InvoiceGenerator invoiceGenerator = new InvoiceGenerator();
                double distance = 2.0;
                int time = 5;
                invoiceGenerator.calculateFare(distance, time);
            }
            catch (CabInvoiceException cie)
            {
                actual = cie.Message;


            }
            Assert.AreEqual(expected, actual);


        }


        //Given the Invalid Distance throw Invalid distance exception
        [Test]
        public void GivenInvalidDistance_ShouldThrowException()
        {
            CabInvoiceException.ExceptionType expected = CabInvoiceException.ExceptionType.INVALID_DISTANCE;
            try
            {
                InvoiceGenerator invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);
                double distance = -2.0;
                int time = 5;
                invoiceGenerator.calculateFare(distance, time);
            }
            catch (CabInvoiceException cie)
            {
                CabInvoiceException.ExceptionType actual = cie.type;
                Assert.AreEqual(expected, actual);
            }


        }

        //Given the Invalid Time throw Invalid time exception
        [Test]
        public void GivenInvalidTime_ShouldThrowException()
        {
            CabInvoiceException.ExceptionType expected = CabInvoiceException.ExceptionType.INVALID_TIME;
            CabInvoiceException.ExceptionType actual;
            try
            {
                InvoiceGenerator invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);
                double distance = 2.0;
                int time = -5;
                invoiceGenerator.calculateFare(distance, time);
            }
            catch (CabInvoiceException cie)
            {
                actual = cie.type;

                Assert.AreEqual(expected, actual);
            }
        }
        [Test]


        //UC2
        //Given mutiple rides return the Invoice Summary class object
        public void GivenMultipleRides_GivesInvoiceSummaryObject()
        {
            Ride[] rides = { new Ride(2, 5), new Ride(2, 5) };
            InvoiceGenerator invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);

            InvoiceSummary actual = invoiceGenerator.calculateFare(rides);

            InvoiceSummary expected = new InvoiceSummary(2, 50);
            Assert.AreEqual(expected, actual);

        }


        //UC3
        //Given mutiple rides return the enhanced invoice 
        [Test]
        public void GivenMutipleRides_GetEnhancedInvoice()
        {
            Ride[] rides = { new Ride(2, 5), new Ride(2, 5) };
            InvoiceGenerator invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);
            InvoiceSummary invoiceSummary = invoiceGenerator.calculateFare(rides);
            int expectedNoOfRides = 2;
          double expectedTotalFare = 50;
            double expectedAvgFare = 25;

            Assert.AreEqual(expectedNoOfRides, invoiceSummary.numberOfRides);
            Assert.AreEqual(expectedTotalFare, invoiceSummary.totalFare);
            Assert.AreEqual(expectedAvgFare, invoiceSummary.avgFare);
        }



        //UC4
        //Given userId get the list of rides and return invoice

        [Test]
        public void GivenUserId_returnTheInvoice()
        {
            Ride[] rides = { new Ride(2, 5), new Ride(2, 5) };
           InvoiceGenerator invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);
            invoiceGenerator.AddRides("100", rides);
          InvoiceSummary actual = invoiceGenerator.calculateFare(rides);

            InvoiceSummary expected = invoiceGenerator.GetInvoiceSummary("100");
            Assert.AreEqual(expected, actual);

        }

    }
}
