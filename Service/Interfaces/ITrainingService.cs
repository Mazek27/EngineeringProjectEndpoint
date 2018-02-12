using System.Collections.Generic;
using System.Threading.Tasks;
using Engineering_Project.Models.Domian;
using Engineering_Project.Service.Security;

namespace Engineering_Project.Service.Interfaces
{
    public interface ITrainingService
    {
        Task<List<Training>> GetTrainingListForMonth(int month, int year, string userName);
    }
}