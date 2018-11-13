using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NpgsqlTypes;

namespace Engineering_Project.Models.Entity
{
    [ComplexType]
    [Table("Trainings")]
    public class Training
    {
        [Key] 
        public int Id { get; set; }
        [Column("UserId")]
        public Guid UserId { get; set; }
        [Column("Type")]
        public int Type { get; set; }
        [Column("TrainingTime")]
        public DateTime TrainingTime { get; set; }
        [Column("Detail")]
        public string Detail { get; set; }
        [Column("Gps")]
        public string Gps { get; set; }
        
    }
}