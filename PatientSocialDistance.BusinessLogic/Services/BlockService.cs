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
                    blockFromDB.IsDeleted = false;
                    blockFromDB.BlockedAt = DateTime.Now;
                }
                else
                {
                    blockFromDB.IsDeleted = true;
                }

            }
            else
            {
                if (blockUserDto.MakeBlock)
                {
                    Block block = new Block
                    {
                        IsDeleted = false,
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

        public async Task<Result> GetAllAsync(string user)
        {
            var result = await _unitOfWork.BlockRepository.GetAllAsync(user);

            if (result.Count() <= 0) return new Result() { Message = ResultMessages.YouDontMakeAnyBlockYet };

            List<string> blockUserDtos = result.ToList().Select(r => _userManager.FindByIdAsync(r.UserBlockedId).Result?.Name).ToList();

            return new Result
            {
                IsCompleted = true,
                Message = ResultMessages.ProcessCompleted,
                Value = blockUserDtos
            };



        }
    }
}
