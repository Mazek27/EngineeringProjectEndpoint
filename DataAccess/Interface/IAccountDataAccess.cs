using System;
using System.Threading.Tasks;
using Engineering_Project.Models.Transmit;
using Engineering_Project.Service.Security;

namespace Engineering_Project.DataAccess
{
    public interface IAccountDataAccess
    {
        Task<bool> AddUser(UserRegisterTransmitModel model);
        Task<object> AddRole(ApplicationRole model);
        Task<bool> DeleteUser(DeleteUserTransmitModel model);
        Task<object> Authenticate(UserSignInTransmitModel model);
//        Task<bool> ResetPassword(UserChangePasswordTrasnmitModel model);
        Task<bool> ChangeRole(AdminChangeRoleTransmitModel model);
        Task<Guid> GetUserIdAsync(string userName);
    }
} 