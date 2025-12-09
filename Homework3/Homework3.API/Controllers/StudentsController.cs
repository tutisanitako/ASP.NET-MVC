using Homework3.Application.DTOs;
using Homework3.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Homework3.API.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudents(
            [FromQuery] string? filter = null,
            [FromQuery] string? orderBy = null,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 15)
        {
            var result = await _studentService.GetStudentsAsync(filter, orderBy, pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudent(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null)
                return NotFound();

            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody] CreateStudentDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdStudent = await _studentService.CreateStudentAsync(dto);
            return CreatedAtAction(nameof(GetStudent), new { id = createdStudent.Id }, createdStudent);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] UpdateStudentDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updatedStudent = await _studentService.UpdateStudentAsync(dto);
                return Ok(updatedStudent);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var result = await _studentService.DeleteStudentAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}