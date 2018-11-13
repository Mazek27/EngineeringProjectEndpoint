using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Engineering_Project.Models.Domian;
using Engineering_Project.Models.Domian.Workout;
using Engineering_Project.Models.Entity;
using Engineering_Project.Models.Transmit.Training;
using Engineering_Project.Service.Context;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Engineering_Project.DataAccess
{
    public class TrainingDataAccess : AbstractDataAccess, ITrainingDataAccess
    {
        public TrainingDataAccess(ApplicationContext applicationContext) : base(applicationContext)
        {
        }
        
        public Task<List<Training>> GetTreningListForMonth(PeriodOfTime periodOfTime, Guid userID)
        {
            var startOfThePeriod = new DateTime(
                periodOfTime.StartOfThePeriod.Year,
                periodOfTime.StartOfThePeriod.Month,
                periodOfTime.StartOfThePeriod.Day,
                0, 0, 0);
            var endOfThePeriod = new DateTime(
                periodOfTime.EndOfThePeriod.Year,
                periodOfTime.EndOfThePeriod.Month,
                periodOfTime.EndOfThePeriod.Day,
                23, 59, 59);

            return ApplicationContext.Trainings
                .AsNoTracking()
                .Where(t => t.TrainingTime >= startOfThePeriod && t.TrainingTime <= endOfThePeriod && t.UserId == userID)
                .ToListAsync();
        }

//        public Task<PeriodOfTime> GetPeriodOfTimeForWorkoutById(int id)
//        {
//            
//        }
        

        public Task<List<WorkoutTransmit>> WorkoutById(int id)
        {
            return ApplicationContext.Trainings
                .Where(l => l.Id == id)
                .OrderByDescending(l => l.TrainingTime)
                .Select(training => new WorkoutTransmit(training))
                .ToListAsync();
        }

        public void InsertTraining(WorkoutDomain workoutDomain)
        {
            Training training = new Training
            {
                Type = (int) workoutDomain.Type,
                UserId = workoutDomain.UserId,
                TrainingTime = workoutDomain.TrainingTime,
                Detail = JsonConvert.SerializeObject(workoutDomain.WorkoutDetail),
                Gps = JsonConvert.SerializeObject(workoutDomain.Localizations)
            };

            ApplicationContext.Trainings.Add(training);
            ApplicationContext.SaveChanges();
        }

        public Task<List<WorkoutTransmit>> TrainingList(Guid userId)
        {
            return ApplicationContext.Trainings
                .Where(t => t.UserId == userId)
                .Select(t => new WorkoutTransmit(t))
                .ToListAsync();
        }
    }
}