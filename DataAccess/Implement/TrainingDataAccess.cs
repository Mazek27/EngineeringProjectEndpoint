using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Engineering_Project.Models.Entity;
using Engineering_Project.Service.Context;
using Microsoft.EntityFrameworkCore;

namespace Engineering_Project.DataAccess
{
    public class TrainingDataAccess : AbstractDataAccess, ITrainingDataAccess
    {
        public TrainingDataAccess(Context context) : base(context)
        {
        }
        
        public Task<List<Training>> GetTreningListForMonth(int month, int year, string userID)
        {
            return _Context.Trainings
                .AsNoTracking()
                .Where(t => t.StartTime.Month == month && t.StartTime.Year == year && t.UserId == userID)
                .ToListAsync();
        }
    }
}