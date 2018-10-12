using System;
using System.Collections.Generic;

namespace Engineering_Project.Models.Domian
{
    public class TrainingDayResult 
    {
        public DateTime Day { get; set; }
        public List<TrainingDetail> Trainings { get; set; }
    }
}