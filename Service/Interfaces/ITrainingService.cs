using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Engineering_Project.Models.Domian;
using Engineering_Project.Models.Transmit.Training;

namespace Engineering_Project.Service.Interfaces
{
    public interface ITrainingService
    {
        Task<List<KeyValuePair<DateTime,List<Training>>>> GetTrainingListForSelectedDate(CurrentDisplayedDate date, string userName);
    }
}