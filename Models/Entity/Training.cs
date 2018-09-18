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
//        [Key]
        public int Id { get; set; }
        [Column("UserId")]
        public Guid UserId { get; set; }
        [Column("Type")]
        public int Type { get; set; }
        [Column("StartTime")]
        public DateTime StartTime { get; set; }
        [Column("FinishTime")]
        public DateTime FinishTime { get; set; }
    }
}