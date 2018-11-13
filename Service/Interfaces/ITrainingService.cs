using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Engineering_Project.Models.Domian;
using Engineering_Project.Models.Domian.Workout;
using Engineering_Project.Models.Transmit.Training;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Engineering_Project.Service.Interfaces
{
    public interface ITrainingService
    {
        Task<List<KeyValuePair<DateTime,DayData>>> TrainingListForSelectedDate(CurrentDisplayedDate date, string userName);
        Task<List<WorkoutTransmit>> WorkoutById(int id);
        Task<bool> InsertTraining(IFormFile file, string userName);
        Task<List<WorkoutTransmit>> TrainingList(string userName);
    }
}