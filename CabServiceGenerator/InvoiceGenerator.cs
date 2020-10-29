using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace CabInvoiceGenerator
{
    public class InvoiceGenerator
    {


        public RideType rideType;
        private RideRepository rideRepository;

        private readonly double MINIMUM_COST_PER_KM;
        private readonly int COST_PER_TIME;
        private readonly double MINIMUM_FARE;

        public InvoiceGenerator(RideType rideType)
        {
            this.rideType = rideType;
            this.rideRepository = new RideRepository();

            if (rideType.Equals(RideType.PREMIUM))
            {
                this.MINIMUM_COST_PER_KM = 15;
                this.COST_PER_TIME = 2;
                this.MINIMUM_FARE = 20;
            }
            else if (rideType.Equals(RideType.NORMAL))
            {
                this.MINIMUM_COST_PER_KM = 10;
                this.COST_PER_TIME = 1;
                this.MINIMUM_FARE = 10;
            }


        }


        public InvoiceGenerator()
        {
            throw new CabInvoiceException(CabInvoiceException.ExceptionType.INVALID_RIDE_TYPE, "Invalid ride type");
        }

        public double calculateFare(double distance, int time)
        {
            double totaFare = 0;



            if (distance <= 0)
            {
                throw new CabInvoiceException(CabInvoiceException.ExceptionType.INVALID_DISTANCE, "Invalid distance");
            }
            if (time < 0)
            {
                throw new CabInvoiceException(CabInvoiceException.ExceptionType.INVALID_TIME, "Invalid Time");
            }
            totaFare = distance * MINIMUM_COST_PER_KM + time * COST_PER_TIME;
            return Math.Max(totaFare, MINIMUM_FARE);
        }

        public InvoiceSummary calculateFare(Ride[] rides)
        {

            double totalFare = 0;
            if (rides == null)
            {
                throw new CabInvoiceException(CabInvoiceException.ExceptionType.NULL_RIDES, "Rides are null");
            }
            foreach (var ride in rides)
            {
                totalFare += this.calculateFare(ride.distance, ride.time);
            }
            return new InvoiceSummary(rides.Length, totalFare);
        }


        public InvoiceSummary GetInvoiceSummary(string userId)
        {
            try
            {
                return this.calculateFare(rideRepository.GetRides(userId));
            }
            catch (CabInvoiceException)
            {
                throw new CabInvoiceException(CabInvoiceException.ExceptionType.INVALID_USER_ID, "Invalid UserId");
            }
        }

        public void AddRides(string userID, Ride[] rides)
        {
            try
            {
                rideRepository.AddRide(userID, rides);
            }
            catch (CabInvoiceException)
            {
                if (rides == null)
                {
                    throw new CabInvoiceException(CabInvoiceException.ExceptionType.NULL_RIDES, "rides are null");
                }
            }
        }





    }
}
