using Microsoft.AspNetCore.Identity;
using PatientSocialDistance.BusinessLogic.Services.IServices;
using PatientSocialDistance.DataAccess.Data;
using PatientSocialDistance.DataAccess.Repository.IRepository;
using PatientSocialDistance.Persistence;
using PatientSocialDistance.Persistence.DTOs;
using PatientSocialDistance.Persistence.Models;
using PatientSocialDistance.Persistence.NotDbModels;

namespace PatientSocialDistance.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> GetUsers(string username)
        {
            
            var usersFromDb = await _unitOfWork.UserRepository.GetbyUsernameAsync(username);
            if (usersFromDb is not null)
            {

                return new Result() { IsCompleted = true, Message = ResultMessages.ProcessCompleted, Value = usersFromDb };
            }
            return new Result() { Message = ResultMessages.NoUserFound };
        }
    }
}
