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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.KeyVault.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Engineering_Project.Controllers
{
    [Route("api/trainings/")]
//    #if !DEBUG
        [Authorize]
//    #endif
    public class TrainingController : Controller
    {
        private readonly ITrainingService _trainingService;

        public TrainingController(ITrainingService trainingService)
        {
            _trainingService = trainingService;
        }

        [HttpGet()]
        public async Task<IActionResult> TrainingList()
        {
            string userName = DebugAuth.getUserName(User);
            try
            {
                var trainingList = await _trainingService.TrainingList(userName);
                return Ok(trainingList);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

        [HttpPost()]
        public async Task<IActionResult> TrainingListForSelectedDate([FromBody] CurrentDisplayedDate currentDisplayedDate)
        {
            string userName = DebugAuth.getUserName(User);
            var list = await _trainingService.TrainingListForSelectedDate(currentDisplayedDate, userName);
            return Ok(new
            {
                data = list,
                date = currentDisplayedDate.currentDate
            });
        }

        [HttpGet("{id}")]
        public async Task<List<WorkoutTransmit>> GetGeoLocalizationForWorkoutById(int id)
        {
            var training = await _trainingService.WorkoutById(id);
            return training;
        }


        [HttpPost("upload_gpx")]
        public async Task<IActionResult> UploadTrainingFromGpxFile(IFormFile FilePayload)
        {
            try
            {
                await _trainingService.InsertTraining(FilePayload, DebugAuth.getUserName(User));
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            

        }
        
        
    }
}