using Homework3.Application.DTOs;
using Homework3.Application.Interfaces;
using Homework3.Domain.Entities;
using Homework3.Domain.Interfaces;

namespace Homework3.Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repository;

        public StudentService(IStudentRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedStudentsDto> GetStudentsAsync(string? filter, string? orderBy, int pageNumber, int pageSize)
        {
            var students = await _repository.GetFilteredAndSortedAsync(filter, orderBy, pageNumber, pageSize);
            var totalCount = await _repository.GetTotalCountAsync(filter);

            return new PaginatedStudentsDto
            {
                Students = students.Select(MapToDto),
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };
        }

        public async Task<StudentDto?> GetStudentByIdAsync(int id)
        {
            var student = await _repository.GetByIdAsync(id);
            return student == null ? null : MapToDto(student);
        }

        public async Task<StudentDto> CreateStudentAsync(CreateStudentDto dto)
        {
            var student = new Student
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                BirthDate = dto.BirthDate,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                ParentName = dto.ParentName,
                City = dto.City,
                Grade = dto.Grade,
                StudentId = dto.StudentId
            };

            var created = await _repository.CreateAsync(student);
            return MapToDto(created);
        }

        public async Task<StudentDto> UpdateStudentAsync(UpdateStudentDto dto)
        {
            var student = new Student
            {
                Id = dto.Id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                BirthDate = dto.BirthDate,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                ParentName = dto.ParentName,
                City = dto.City,
                Grade = dto.Grade,
                StudentId = dto.StudentId
            };

            var updated = await _repository.UpdateAsync(student);
            return MapToDto(updated);
        }

        public async Task<bool> DeleteStudentAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        private static StudentDto MapToDto(Student student)
        {
            return new StudentDto
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                BirthDate = student.BirthDate,
                Email = student.Email,
                PhoneNumber = student.PhoneNumber,
                ParentName = student.ParentName,
                City = student.City,
                Grade = student.Grade,
                StudentId = student.StudentId
            };
        }
    }
}