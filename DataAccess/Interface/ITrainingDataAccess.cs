using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Engineering_Project.Models.Domian.Workout;
using Engineering_Project.Models.Entity;
using Engineering_Project.Models.Transmit.Training;

namespace Engineering_Project.DataAccess
{
    public interface ITrainingDataAccess
    {
        Task<List<Training>> GetTreningListForMonth(PeriodOfTime periodOfTime, Guid userID);
        Task<List<WorkoutGeoLocalization>> GetGeoLocalizationForWorkoutById(int id);
    }
}