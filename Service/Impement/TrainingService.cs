using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Engineering_Project.DataAccess;
using Engineering_Project.Models.Domian;
using Engineering_Project.Models.Enums;
using Engineering_Project.Service.Interfaces;

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


        public async Task<List<Training>> GetTrainingListForMonth(int month, int year, string userName)
        {
            string userID = await _accountDataAccess.GetUserIdAsync(userName);
            
            var trainingList = await _trainingDataAccess.GetTreningListForMonth(month, year, userID);
            var output = trainingList.Select(t => new Training
            {
                StartTime = t.StartTime.ToDateTime(),
                Duration = (int)(t.FinishTime.ToDateTime() - t.StartTime.ToDateTime()).TotalSeconds,
                Type = TrainingType.WALKING,
                Distance = 13.2
            }).ToList();
            return output;
        }
    }
}