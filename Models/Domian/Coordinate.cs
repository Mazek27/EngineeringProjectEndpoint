using System;
using System.Collections.Generic;

namespace Engineering_Project.Models.Domian
{
    
    public class Coordinate
    {
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public DateTime Measurement { get; private set; }

        public Coordinate(double latitude, double longitude, DateTime measurement)
        {
            Latitude = latitude;
            Longitude = longitude;
            Measurement = measurement;
        }   
    }
    public static class CoordinatesDistanceExtensions
    {
        public static double DistanceTo(this Coordinate baseCoordinate, Coordinate targetCoordinate)
        {
            return DistanceTo(baseCoordinate, targetCoordinate, UnitOfLength.Kilometers);
        }

        public static double DistanceTo(this Coordinate baseCoordinate, Coordinate targetCoordinate, UnitOfLength unitOfLength)
        {
            var baseRad = Math.PI * baseCoordinate.Latitude / 180;
            var targetRad = Math.PI * targetCoordinate.Latitude/ 180;
            var theta = baseCoordinate.Longitude - targetCoordinate.Longitude;
            var thetaRad = Math.PI * theta / 180;

            double dist =
                Math.Sin(baseRad) * Math.Sin(targetRad) + Math.Cos(baseRad) *
                Math.Cos(targetRad) * Math.Cos(thetaRad);
            dist = Math.Acos(dist);

            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;

            return unitOfLength.ConvertFromMiles(dist);
        }
    }

    public class UnitOfLength
    {
        public static UnitOfLength Kilometers = new UnitOfLength(1.609344);
        public static UnitOfLength NauticalMiles = new UnitOfLength(0.8684);
        public static UnitOfLength Miles = new UnitOfLength(1);

        private readonly double _fromMilesFactor;

        private UnitOfLength(double fromMilesFactor)
        {
            _fromMilesFactor = fromMilesFactor;
        }

        public double ConvertFromMiles(double input)
        {
            return input*_fromMilesFactor;
        }
    } 
}