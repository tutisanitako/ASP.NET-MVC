using Homework3.Domain.Entities;
using Homework3.Domain.Interfaces;
using Homework3.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Homework3.Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;

        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<IEnumerable<Student>> GetFilteredAndSortedAsync(string? filter, string? orderBy, int pageNumber, int pageSize)
        {
            var query = _context.Students.AsQueryable();

            // Apply filtering
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(s => s.FirstName.Contains(filter) || s.LastName.Contains(filter));
            }

            // Apply sorting
            query = orderBy?.ToLower() switch
            {
                "name_asc" => query.OrderBy(s => s.FirstName).ThenBy(s => s.LastName),
                "name_desc" => query.OrderByDescending(s => s.FirstName).ThenByDescending(s => s.LastName),
                "date_asc" => query.OrderBy(s => s.BirthDate),
                "date_desc" => query.OrderByDescending(s => s.BirthDate),
                _ => query.OrderBy(s => s.Id)
            };

            // Apply pagination
            return await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalCountAsync(string? filter)
        {
            var query = _context.Students.AsQueryable();

            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(s => s.FirstName.Contains(filter) || s.LastName.Contains(filter));
            }

            return await query.CountAsync();
        }

        public async Task<Student?> GetByIdAsync(int id)
        {
            return await _context.Students.FindAsync(id);
        }

        public async Task<Student> CreateAsync(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return student;
        }

        public async Task<Student> UpdateAsync(Student student)
        {
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
            return student;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
                return false;

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}