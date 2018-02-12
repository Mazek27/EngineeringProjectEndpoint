using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NpgsqlTypes;

namespace Engineering_Project.Models.Entity
{
    [ComplexType]
    [Table("Training")]
    public class Training
    {
        [Key] 
//        [Key]
        public int Id { get; set; }
        [Column("UserId")]
        public string UserId { get; set; }
        [Column("Type")]
        public int Type { get; set; }
        [Column("StartTime")]
        public NpgsqlDateTime StartTime { get; set; }
        [Column("FinishTime")]
        public NpgsqlDateTime FinishTime { get; set; }
    }
}