using Blazor.Learner.Server.Data;
using Blazor.Learner.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Learner.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeveloperController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public DeveloperController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var developers = await _dbContext.Developers.ToListAsync();
            return Ok(developers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDeveloper(int id)
        {
            var developer = await _dbContext.Developers.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(developer);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Developer developer)
        {
            _dbContext.Developers.Add(developer);
            await _dbContext.SaveChangesAsync();
            return Ok(developer.Id);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Developer developer)
        {
            _dbContext.Entry(developer).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var developer = _dbContext.Developers.Find(id);
            _dbContext.Remove(developer);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
