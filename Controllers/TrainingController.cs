using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Engineering_Project.Models.Transmit.Training;
using Engineering_Project.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Engineering.Controllers
{
    [Route("api/training/")]
    [Authorize]
    public class TrainingController : Controller
    {
        private readonly ITrainingService _trainingService;

        public TrainingController(ITrainingService trainingService)
        {
            _trainingService = trainingService;
        }


        [HttpPost("date")]
        public async Task<IActionResult> TrainingListForMonth([FromBody] Period period)
        {
            string userName = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var list = await _trainingService.GetTrainingListForMonth(period.Month, period.Year, userName);
            return Ok(new {
                data = list,
                month = period.Month,
                year = period.Year
            });
        }
    }
}