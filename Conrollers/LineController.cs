using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SteamQueue.Context;
using SteamQueue.DTOs;
using SteamQueue.Entities;

namespace SteamQueue.Conrollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LineController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public LineController(IMapper mapper, DatabaseContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Line>>> GetAll()
        {
            var lines = await _context
               !.Lines
               !.ToListAsync();

            return lines;
        }

        [HttpGet("GetInfo/{lineId}")]
        public async Task<ActionResult<Line>> GetInfo(Guid lineId)
        {
            var lineInfo = await _context
                !.Lines
                !.Where(line => line.Id == lineId)
                .FirstOrDefaultAsync();

            if (lineInfo is null)
                return NotFound();

            return Ok(lineInfo);
        }

        [HttpPost("Add/{accountId}")]
        public async Task<ActionResult<Line>> Add(string accountId, [FromBody]AddLineDto line)
        {
            var accountToAdd = _context.Accounts.Where(acc => acc.Id == Guid.Parse(accountId)).FirstOrDefault();
            var lineToAdd = _mapper.Map<Line>(line);
            lineToAdd.SteamAccount = accountToAdd;
            _context
                !.Lines
                !.Add(lineToAdd);

            _context!.SaveChanges();

            return Ok();
        }
    }
}
