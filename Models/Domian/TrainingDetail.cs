using System;
using System.Collections.Generic;
using Engineering_Project.Models.Enums;
using NpgsqlTypes;

namespace Engineering_Project.Models.Domian
{
    public class TrainingDetail
    {
        public TrainingDetail()
        {
            Localizations = new List<GeoCoordinate>();
        }

        public int Id { get; set; }
        public TrainingType Type { get; set; }
        public DateTime TrainingTime { get; set; }
        public double Distance { get; set; }
        public double Duration { get; set; }
        public List<GeoCoordinate> Localizations{get;set;}
    }
}