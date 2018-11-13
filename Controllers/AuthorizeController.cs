using System;
using System.Threading.Tasks;
using Engineering_Project.DataAccess;
using Engineering_Project.Models.Transmit;
using Engineering_Project.Service.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Engineering_Project.Controllers
{
    [Route("api/users/")]
//    [Authorize]
    public class AuthorizeController : Controller
    {
        private readonly IAccountDataAccess _dataAccess;

        public AuthorizeController(IAccountDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        [HttpPost("register")]
//        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddUser([FromBody] UserRegisterTransmitModel model)
        {
            model.Locale =  this.Request.Headers["Accept-Language"].ToString().Split(",")[0];
            model.ApplicationRoleName = "User";
            if (!ModelState.IsValid || !await _dataAccess.AddUser(model))
            {
                return BadRequest("Invalid user definition");
            }

            return Ok();
        }

        [HttpPost("DeleteUser")]
//        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser([FromBody] DeleteUserTransmitModel model)
        {
            if (await _dataAccess.DeleteUser(model))
                return StatusCode(200, "Deleted");
            return StatusCode(400, "Unknown _error");
        }

        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] UserSignInTransmitModel model)
        {
            object token = await _dataAccess.Authenticate(model);
            if (token != null)
            {
                Response.ContentType = "application/json";
                return StatusCode(200, token);
            }
            return StatusCode(404);
        }

        [HttpPost("SignOut")]
        public IActionResult SignOut()
        {
            return StatusCode(200, "Signed out");
        }

        [HttpPost("AddRole")]
//        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddRole([FromBody] AddRoleTransmitModel model)
        {
            try
            {
                ApplicationRole role = new ApplicationRole();
                role.Id = Guid.NewGuid();
                role.Name = model.RoleName;

                
                return StatusCode(200, await _dataAccess.AddRole(role));
                
                    
            }
            catch (System.Exception e)
            {
                return StatusCode(404, e.Message);
            }
            return StatusCode(400, "Unknown _error");
        }

//        [HttpPost("ChangeTokenTime")]
//        [Authorize(Roles = "Admin")]
//        public async Task<IActionResult> ChangeTokenTime(int time)
//        {
//            if (await _dataAccess.ChangeTokenTime(time))
//            {
//                return StatusCode(200, "Time changed");
//            }
//            return StatusCode(418, "Something bad");
//        }

        [HttpPost("ChangeRole")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeRoles(AdminChangeRoleTransmitModel model)
        {
            if (await _dataAccess.ChangeRole(model))
            {
                return StatusCode(200, "Role changed");
            }
            return StatusCode(418, "Something bad");
        }

//        [HttpPost("ResetPassword")]
//        public async Task<IActionResult> ResetPassword(UserChangePasswordTrasnmitModel model)
//        {
//            if (await _dataAccess.ResetPassword(model))
//            {
//                return StatusCode(200, "Password changed");
//            }
//            return StatusCode(418, "Something bad");
//        }
    }
}