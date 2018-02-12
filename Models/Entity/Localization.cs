using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Engineering_Project.Models.Entity
{
    [Table("Localization")]
    public class Localization
    {
        [Column("UserId")]
        public string UserId;
        [Column("MeasurementTime")]
        public DateTime MeasurementTime;
        [Column("Lat")]
        public double Lat;
        [Column("Lng")]
        public double Lng;
    }
}