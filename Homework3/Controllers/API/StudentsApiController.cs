using Homework3.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Homework3.Controllers.API
{
    [Route("api/students")]
    [ApiController]
    public class StudentsApiController : ControllerBase
    {
        private readonly IRepository _repository;

        public StudentsApiController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudents(
            [FromQuery] string? filter = null,
            [FromQuery] string? orderBy = null,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 15)
        {
            var students = await _repository.GetFilteredAndSortedAsync(filter, orderBy, pageNumber, pageSize);
            var totalCount = await _repository.GetTotalCountAsync(filter);

            var result = new
            {
                Students = students,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };

            return Ok(result);
        }
    }
}