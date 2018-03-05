using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Engineering_Project.Models.Domian.Workout;
using Engineering_Project.Models.Transmit.Training;
using Engineering_Project.Service.Interfaces;
using Engineering_Project.Service.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.KeyVault.Models;

namespace Engineering_Project.Controllers
{
    [Route("api/trainings/")]
    #if !DEBUG
        [Authorize]
    #endif
    public class TrainingController : Controller
    {
        private readonly ITrainingService _trainingService;

        public TrainingController(ITrainingService trainingService)
        {
            _trainingService = trainingService;
        }


        [HttpPost()]
        public async Task<IActionResult> TrainingListForSelectedDate([FromBody] CurrentDisplayedDate currentDisplayedDate)
        {
            string userName = DebugAuth.getUserName(User);
            var list = await _trainingService.GetTrainingListForSelectedDate(currentDisplayedDate, userName);
            return Ok(new
            {
                data = list,
                date = currentDisplayedDate.currentDate
            });
        }

        [HttpGet("{id}")]
        public async Task<List<WorkoutGeoLocalization>> GetGeoLocalizationForWorkoutById(int id)
        {
            return await _trainingService.GetGeoLocalizationForWorkoutById(id);
        }
        
        
    }
}