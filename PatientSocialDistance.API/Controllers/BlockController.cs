﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatientSocialDistance.BusinessLogic.Services.IServices;
using PatientSocialDistance.Persistence.DTOs;

namespace PatientSocialDistance.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlockController : ControllerBase
    {
        private readonly IBlockService _blockService;

        public BlockController(IBlockService blockService)
        {
            _blockService = blockService;
        }
        [HttpGet("GellBlockByUser")]
        public async Task<IActionResult> GetAllByUser(string UserId) 
        {
            var result = await _blockService.GetAllAsync(UserId);

            if (!result.IsCompleted) return BadRequest(result.Message);

            return Ok(result.Value);
        }

        [HttpPost("CreateOrDeleteBlock")]
        public async Task<IActionResult> CreateOrDeleteBlock(BlockUserDto blockUserDto)
        {
            var result = await _blockService.CreateOrDeleteBlockAsync(blockUserDto);

            if (!result.IsCompleted) return BadRequest(result.Message);

            return Ok(result.Value);
        }
    }
}