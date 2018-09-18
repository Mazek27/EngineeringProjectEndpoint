using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Engineering_Project.DataAccess;
using Engineering_Project.Models.Domian;
using Engineering_Project.Models.Domian.Workout;
using Engineering_Project.Models.Enums;
using Engineering_Project.Models.Transmit.Training;
using Engineering_Project.Service.Interfaces;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Engineering_Project.Service.Impement
{
    public class TrainingService : ITrainingService
    {
        private readonly ITrainingDataAccess _trainingDataAccess;
        private readonly IAccountDataAccess _accountDataAccess;

        public TrainingService(ITrainingDataAccess trainingDataAccess, IAccountDataAccess accountDataAccess)
        {
            _trainingDataAccess = trainingDataAccess;
            _accountDataAccess = accountDataAccess;
        }


        public async Task<List<KeyValuePair<DateTime, DayData>>> GetTrainingListForSelectedDate(CurrentDisplayedDate date, string userName)
        {
            Guid userID = await _accountDataAccess.GetUserIdAsync(userName);

            var periodOfTime = GetPeriodOfTimeForCurrentMonth(date.currentDate);
            
            var trainingList = await _trainingDataAccess.GetTreningListForMonth(periodOfTime , userID);
            var dayList = GetDateListByPeriodOfTime(periodOfTime);

            var output = dayList.ToDictionary(time => time.Date, time =>
                new DayData
                {
                    Type = time.Month == date.currentDate.Month ? 'c' : time.Month < date.currentDate.Month ? 'p' : 'n',
                    TrainingList = trainingList
                        .Where(t => t.StartTime.Date == time.Date)
                        .Select(t => new Training
                        {
                            Id = t.Id,
                            TrainingTime = t.StartTime,
                            Duration = (int) (t.FinishTime - t.StartTime).TotalSeconds,
                            Type = (TrainingType) t.Type,
                            Distance = 13.2
                        }).ToList()
                }).ToList();
            
            return output;
        }

        public PeriodOfTime GetPeriodOfTimeForCurrentMonth(DateTime date)
        {
            var startDayOfWeek = new DateTime(date.Year, date.Month, 1);
            var day = startDayOfWeek.DayOfWeek;

            var startDay = startDayOfWeek.AddDays(-(int)day);

            return new PeriodOfTime
            {
                StartOfThePeriod = startDay,
                EndOfThePeriod = new DateTime(startDay.AddDays(41).Ticks)
            };
        }

        public List<DateTime> GetDateListByPeriodOfTime(PeriodOfTime periodOfTime)
        {
            return Enumerable.Range(0, 1 + periodOfTime.EndOfThePeriod.Subtract(periodOfTime.StartOfThePeriod).Days)
                .Select(offset => periodOfTime.StartOfThePeriod.AddDays(offset).Date)
                .ToList(); 
        }

        public async Task<List<WorkoutGeoLocalization>> GetGeoLocalizationForWorkoutById(int id)
        {
            return await _trainingDataAccess.GetGeoLocalizationForWorkoutById(id);
        }
    }
}