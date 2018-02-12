using System.Collections.Generic;
using System.Threading.Tasks;
using Engineering_Project.Models.Entity;

namespace Engineering_Project.DataAccess
{
    public interface ITrainingDataAccess
    {
        Task<List<Training>> GetTreningListForMonth(int month, int year, string userID);
    }
}