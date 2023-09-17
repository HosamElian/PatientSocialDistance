﻿using Microsoft.EntityFrameworkCore;
using PatientSocialDistance.DataAccess.Data;
using PatientSocialDistance.DataAccess.Repository.IRepository;
using PatientSocialDistance.Persistence.Models;

namespace PatientSocialDistance.DataAccess.Repository
{
    public class BlockRepository : IBlockRepository
    {
        private readonly ApplicationDbContext _context;

        public BlockRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(Block block)
        {
            _context.Blocks.Add(block);
        }

        public async Task<IEnumerable<Block>> GetAllAsync(string UserId)
        {
            var query = _context.Blocks.AsQueryable();
            return await query.Where(x => x.UserMakeBlockId == UserId && !x.IsDeleted).ToListAsync();
        }

        public async Task<Block> GetByUserIdIdAsync(string UserId, string UserBlockedId)
        {
            return await _context.Blocks.FirstOrDefaultAsync(b => b.UserMakeBlockId == UserId && b.UserBlockedId == UserBlockedId && !b.IsDeleted);
        }
    }
}