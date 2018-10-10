using System.Collections.Generic;
using System.Linq;
using Engineering_Project.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Primitives;

namespace Engineering_Project.Controllers
{
    [Route("api/languages/")]
    [Authorize]
    public class LanguagesController : Controller
    {
        private IInternationalizationService _internationalizationService;

        public LanguagesController(IInternationalizationService internationalizationService)
        {
            _internationalizationService = internationalizationService;
        }

        [HttpGet()]
        public object GetLanguages()
        {
            return _internationalizationService.SelectLanguage(Request.Headers["Accept-Language"]);
        }
    }
}