using System.Net;
using AutoMapper;
using Domain.ApiResponse;
using Domain.DTOs.StudentDTOs;
using Domain.Entites;
using Domain.Filters;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class StudentService(DataContext context, IMapper mapper) : IStudentService
{
    public async Task<Response<StudentDTO>> GetByIdAsync(int id)
    {
        var student = await context.Students.FindAsync(id);
        if (student == null)
            return new Response<StudentDTO>(HttpStatusCode.NotFound, "Student not found");

        var dto = mapper.Map<StudentDTO>(student);
        return new Response<StudentDTO>(dto);
    }

    public async Task<PagedResponse<List<StudentDTO>>> GetAllAsync(StudentFilter filter)
    {
        var query = context.Students.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.FirstName))
            query = query.Where(s => s.FirstName.ToLower().Contains(filter.FirstName.ToLower()));

        if (!string.IsNullOrWhiteSpace(filter.LastName))
            query = query.Where(s => s.LastName.ToLower().Contains(filter.LastName.ToLower()));

        int totalResults = await query.CountAsync();

        var students = await query
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();

        var dtos = mapper.Map<List<StudentDTO>>(students);
        var totalPages = (int)Math.Ceiling(totalResults / (double)filter.PageSize);

        return new PagedResponse<List<StudentDTO>>(dtos, totalPages, filter.PageNumber, filter.PageSize);
    }

    public async Task<Response<string>> CreateAsync(CreateStudentDTO create)
    {
        if (string.IsNullOrWhiteSpace(create.FirstName) || string.IsNullOrWhiteSpace(create.LastName))
            return new Response<string>(HttpStatusCode.BadRequest, "First name and last name are required");

        var student = mapper.Map<Student>(create);

        context.Students.Add(student);
        await context.SaveChangesAsync();

        return new Response<string>(null!, "Student created successfully");
    }

    public async Task<Response<string>> UpdateAsync(StudentDTO update)
    {
        var student = await context.Students.FindAsync(update.StudentId);
        if (student == null)
            return new Response<string>(HttpStatusCode.NotFound, "Student not found");

        if (string.IsNullOrWhiteSpace(update.FirstName) || string.IsNullOrWhiteSpace(update.LastName))
            return new Response<string>(HttpStatusCode.BadRequest, "First name and last name are required");

        mapper.Map(update, student);
        await context.SaveChangesAsync();

        return new Response<string>(null!, "Student updated successfully");
    }

    public async Task<Response<string>> DeleteAsync(int id)
    {
        var student = await context.Students.FindAsync(id);
        if (student == null)
            return new Response<string>(HttpStatusCode.NotFound, "Student not found");

        context.Students.Remove(student);
        await context.SaveChangesAsync();

        return new Response<string>(null!, "Student deleted successfully");
    }

    public async Task<Response<List<StudentCourseCountDTO>>> GetStudentCourseCountsAsync()
    {
        var data = await context.Students
            .Select(s => new StudentCourseCountDTO
            {
                StudentId = s.StudentId,
                FullName = s.FirstName + " " + s.LastName,
                CourseCount = s.Enrollments.Count
            })
            .ToListAsync();

        return new Response<List<StudentCourseCountDTO>>(data);
    }

    public async Task<Response<List<StudentDTO>>> GetStudentsWithoutCoursesAsync()
    {
        var students = await context.Students
            .Where(s => !s.Enrollments.Any())
            .ToListAsync();

        var dtos = mapper.Map<List<StudentDTO>>(students);

        return new Response<List<StudentDTO>>(dtos);
    }
}
