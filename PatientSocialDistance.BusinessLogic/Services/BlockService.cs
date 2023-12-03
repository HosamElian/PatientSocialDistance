using Microsoft.AspNetCore.Identity;
using PatientSocialDistance.BusinessLogic.Services.IServices;
using PatientSocialDistance.DataAccess.Repository.IRepository;
using PatientSocialDistance.Persistence;
using PatientSocialDistance.Persistence.DTOs;
using PatientSocialDistance.Persistence.Models;
using PatientSocialDistance.Persistence.NotDbModels;

namespace PatientSocialDistance.BusinessLogic.Services
{
    public class BlockService : IBlockService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly INotificationService _notificationService;

        public BlockService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, INotificationService notificationService )
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _notificationService = notificationService;
        }
        public async Task<Result> CreateOrDeleteBlockAsync(BlockUserDTO blockUserDto)
        {
            var user = _userManager.FindByNameAsync(blockUserDto.UsernameMakeBlock).Result;
            var userBlocked = _userManager.FindByNameAsync(blockUserDto.UsernameBlocked).Result;

            var blockFromDB = await _unitOfWork.BlockRepository.GetByUserIdIdAsync(user.Id, userBlocked.Id);

            if (blockFromDB != null)
            {
                if (blockUserDto.MakeBlock)
                {
                    blockFromDB.IsActive = true;
                    blockFromDB.BlockedAt = DateTime.Now;
                    if(blockUserDto.HasNotification)
                    {
                       await _notificationService.AddNotificationNoSave(new NotificationDTO
                        {
                            MakeActionUsername = user.UserName,
                            TargetUsername = userBlocked.UserName,
                            Message = $"You Have Been Blocked by {user.UserName}"
                        });
                    }
                }
                else
                {
                    blockFromDB.IsActive = false;
                }

            }
            else
            {
                if (blockUserDto.MakeBlock)
                {
                    Block block = new()
                    {
                        IsDeleted = false,
                        IsActive = true,
                        BlockedAt = DateTime.Now,
                        UserBlockedId = userBlocked.Id,
                        UserMakeBlockId = user.Id,
                    };
                    if (blockUserDto.HasNotification)
                    {
                        await _notificationService.AddNotificationNoSave(new NotificationDTO
                        {
                            MakeActionUsername = user.UserName,
                            TargetUsername = userBlocked.UserName,
                            Message = $"You Have Been Blocked by {user.UserName}"
                        });
                    }
                    _unitOfWork.BlockRepository.Add(block);
                }
            }

            if (_unitOfWork.Save().Result)
            {
                return new Result
                {
                    IsCompleted = true,
                    Message = ResultMessages.ProcessCompleted,
                    Value = blockUserDto
                };
            }
            return new Result();

        }

        public async Task<bool> CheckBlockAsync(string userWantToCheck, string userTockeckIt)
        {
            var blockFromDB = await _unitOfWork.BlockRepository.GetByUserIdIdAsync(userWantToCheck, userTockeckIt);
            if (blockFromDB != null) return true;
            return false;
        }

        public async Task<Result> GetAllAsync(string username)
        {
            var user = _userManager.FindByNameAsync(username).Result;
            if (user == null) return new Result(); 
            var result = await _unitOfWork.BlockRepository.GetAllAsync(user.Id);

            if (!result.Any()) return new Result() { Message = ResultMessages.YouDontMakeAnyBlockYet };

            List<BlockedUserDTO> blockUserDtos = result.ToList().Select(b=> new BlockedUserDTO
            {
                Id = b.UserBlocked.Id,
                Name = b.UserBlocked.UserName,
                Hospital = b.UserBlocked.Hospital,
                PhoneNumber = b.UserBlocked.PhoneNumber ?? "",
            }).ToList();

            return new Result
            {
                IsCompleted = true,
                Message = ResultMessages.ProcessCompleted,
                Value = blockUserDtos
            };



        }
    }
}
