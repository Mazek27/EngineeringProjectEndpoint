using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Engineering_Project.Models.Domian.Workout;
using Engineering_Project.Models.Entity;
using Engineering_Project.Models.Transmit.Training;
using Engineering_Project.Service.Context;
using Microsoft.EntityFrameworkCore;

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

            return null;
            

//            return _Context.Trainings
//                .AsNoTracking()
//                .Where(t => t.StartTime >= startOfThePeriod && t.FinishTime <= endOfThePeriod && t.UserId == userID)
//                .ToListAsync();
        }

//        public Task<PeriodOfTime> GetPeriodOfTimeForWorkoutById(int id)
//        {
//            
//        }
        

        public Task<List<WorkoutGeoLocalization>> GetGeoLocalizationForWorkoutById(int id)
        {
            return null;
//            return _Context.Localizations
//                .Where(l => l.TrainingId == id)
//                .OrderByDescending(l => l.MeasurementTime)
//                .Select(l => new WorkoutGeoLocalization
//                {
//                    Time = l.MeasurementTime,
//                    Lat = l.Lat,
//                    Lng = l.Lng
//                }).ToListAsync();
        }
    }
}