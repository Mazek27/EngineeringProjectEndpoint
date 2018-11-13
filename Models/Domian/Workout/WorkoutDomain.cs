using System;
using System.Collections.Generic;
using Engineering_Project.Models.Domian.Workout;
using Engineering_Project.Models.Enums;
using NpgsqlTypes;

namespace Engineering_Project.Models.Domian
{
    public class WorkoutDomain
    {
        public WorkoutDomain()
        {
            Localizations = new List<Coordinate>();
        }
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public TrainingType Type { get; set; }
        public DateTime TrainingTime { get; set; }
        public WorkoutDetail WorkoutDetail { get; set; }
        public List<Coordinate> Localizations{get; set;}
    }
}