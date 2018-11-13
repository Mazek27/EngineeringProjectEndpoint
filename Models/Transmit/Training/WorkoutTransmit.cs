using System;
using System.Collections.Generic;
using Engineering_Project.Models.Domian;
using Engineering_Project.Models.Domian.Workout;
using Newtonsoft.Json;

namespace Engineering_Project.Models.Transmit.Training
{
    public class WorkoutTransmit
    {
        public WorkoutTransmit(Entity.Training training)
        {
            Id = training.Id;
            Type = training.Type;
            TrainingTime = training.TrainingTime;
            Detail = JsonConvert.DeserializeObject<WorkoutDetail>(training.Detail);
            Gps = JsonConvert.DeserializeObject<List<Coordinate>>(training.Gps);
        }

        public WorkoutTransmit()
        {
            
        }

        public int Id { get; set; }
        public int Type { get; set; }
        public DateTime TrainingTime { get; set; }
        public WorkoutDetail Detail { get; set; }
        public List<Coordinate> Gps { get; set; }
    }
}