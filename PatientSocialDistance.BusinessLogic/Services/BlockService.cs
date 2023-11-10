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

        public BlockService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        public async Task<Result> CreateOrDeleteBlockAsync(BlockUserDto blockUserDto)
        {
            var blockFromDB = await _unitOfWork.BlockRepository.GetByUserIdIdAsync(blockUserDto.UserMakeBlockId, blockUserDto.UserBlockedId);
            if (blockFromDB != null)
            {
                if (blockUserDto.MakeBlock)
                {
                    blockFromDB.IsActive = true;
                    blockFromDB.BlockedAt = DateTime.Now;
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
                        UserBlockedId = blockUserDto.UserBlockedId,
                        UserMakeBlockId = blockUserDto.UserMakeBlockId,
                    };
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
                Name = b.UserBlocked.Name,
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
