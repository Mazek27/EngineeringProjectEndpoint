using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Engineering_Project.Models.Entity
{
    [Table("Localizations")]
    public class Localization
    {
        [Column("Id")]
        public int Id { get; set; }
        [Column("MeasurementTime")]
        public DateTime MeasurementTime { get; set; }
        [Column("Lat")]
        public double Lat { get; set; }
        [Column("Lng")]
        public double Lng { get; set; }
        [Column("Training_Id")]
        public int TrainingId { get; set; }
    }
}