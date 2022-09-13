using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SteamQueue.Context;
using SteamQueue.Entities;

namespace SteamQueue.Conrollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Accountontroller : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public Accountontroller(IMapper mapper, DatabaseContext context)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpPost("Add")]
        public async Task<SteamAccount> SetAccount(AddAccountDto accountDto)
        {
            var account = _mapper.Map<SteamAccount>(accountDto);
            accountDto.Id = Guid.NewGuid();

            _context!.Accounts!.Add(account);
            int result = await _context.SaveChangesAsync();
            return account;
        }
    }

}