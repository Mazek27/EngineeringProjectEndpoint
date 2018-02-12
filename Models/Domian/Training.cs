using System;
using Engineering_Project.Models.Enums;
using NpgsqlTypes;

namespace Engineering_Project.Models.Domian
{
    public class Training
    {
        public TrainingType Type { get; set; }
        public DateTime StartTime { get; set; }
        public double Distance { get; set; }
        public int Duration { get; set; }
    }
}