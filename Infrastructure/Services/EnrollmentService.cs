using System.Net;
using AutoMapper;
using Domain.ApiResponse;
using Domain.DTOs.CourseDTOs;
using Domain.DTOs.EnrollmentDTOs;
using Domain.DTOs.StudentDTOs;
using Domain.Entites;
using Domain.Filters;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class EnrollmentService(DataContext context, IMapper mapper) : IEnrollmentService
{
    public async Task<Response<EnrollmentDTO>> GetByIdAsync(int id)
    {
        var enrollment = await context.Enrollments
            .Include(e => e.Student)
            .Include(e => e.Course)
            .FirstOrDefaultAsync(e => e.EnrollmentId == id);

        if (enrollment == null)
            return new Response<EnrollmentDTO>(HttpStatusCode.NotFound, "Enrollment not found");

        var dto = mapper.Map<EnrollmentDTO>(enrollment);
        return new Response<EnrollmentDTO>(dto);
    }

    public async Task<PagedResponse<List<EnrollmentDTO>>> GetAllAsync(EnrollmentFilter filter)
    {
        var query = context.Enrollments
            .Include(e => e.Student)
            .Include(e => e.Course)
            .AsQueryable();

        if (filter.StudentId.HasValue)
            query = query.Where(e => e.StudentId == filter.StudentId);

        if (filter.CourseId.HasValue)
            query = query.Where(e => e.CourseId == filter.CourseId);

        int total = await query.CountAsync();

        var list = await query
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();

        var dto = mapper.Map<List<EnrollmentDTO>>(list);
        var totalPages = (int)Math.Ceiling(total / (double)filter.PageSize);

        return new PagedResponse<List<EnrollmentDTO>>(dto, totalPages, filter.PageNumber, filter.PageSize);
    }

    public async Task<Response<string>> CreateAsync(CreateEnrollmentDTO create)
    {
        if (!await context.Students.AnyAsync(s => s.StudentId == create.StudentId))
            return new Response<string>(HttpStatusCode.BadRequest, "Student not found");

        if (!await context.Courses.AnyAsync(c => c.CourseId == create.CourseId))
            return new Response<string>(HttpStatusCode.BadRequest, "Course not found");

        var enrollment = mapper.Map<Enrollment>(create);
        context.Enrollments.Add(enrollment);
        await context.SaveChangesAsync();

        return new Response<string>(null!, "Enrollment created successfully");
    }

    public async Task<Response<string>> UpdateAsync(EnrollmentDTO update)
    {
        var enrollment = await context.Enrollments.FindAsync(update.EnrollmentId);
        if (enrollment == null)
            return new Response<string>(HttpStatusCode.NotFound, "Enrollment not found");

        mapper.Map(update, enrollment);
        await context.SaveChangesAsync();

        return new Response<string>(null!, "Enrollment updated successfully");
    }

    public async Task<Response<string>> DeleteAsync(int id)
    {
        var enrollment = await context.Enrollments.FindAsync(id);
        if (enrollment == null)
            return new Response<string>(HttpStatusCode.NotFound, "Enrollment not found");

        context.Enrollments.Remove(enrollment);
        await context.SaveChangesAsync();

        return new Response<string>(null!, "Enrollment deleted successfully");
    }

    // === Статистика: Студенты, не записанные ни на один курс ===
    public async Task<Response<List<StudentDTO>>> GetStudentsWithoutCoursesAsync()
    {
        var students = await context.Students
            .Where(s => !context.Enrollments.Any(e => e.StudentId == s.StudentId))
            .Select(s => new StudentDTO
            {
                StudentId = s.StudentId,
                FirstName = s.FirstName,
                LastName = s.LastName
            })
            .ToListAsync();

        return new Response<List<StudentDTO>>(students);
    }

    // === Статистика: Кол-во студентов на каждом курсе ===
    public async Task<Response<List<CourseStudentCountDTO>>> GetStudentCountsPerCourseAsync()
    {
        var result = await context.Courses
            .Select(c => new CourseStudentCountDTO
            {
                CourseId = c.CourseId,
                Title = c.Title,
                StudentCount = context.Enrollments.Count(e => e.CourseId == c.CourseId)
            })
            .ToListAsync();

        return new Response<List<CourseStudentCountDTO>>(result);
    }
    public async Task<Response<List<CourseAverageGradeDTO>>> GetAverageGradesPerCourseAsync()
    {
        var result = await context.Courses
            .Select(c => new CourseAverageGradeDTO
            {
                CourseId = c.CourseId,
                Title = c.Title,
                AverageGrade = context.Enrollments
                    .Where(e => e.CourseId == c.CourseId)
                    .Average(e => (double?)e.Grade) ?? 0
            })
            .ToListAsync();

        return new Response<List<CourseAverageGradeDTO>>(result);
    }
    public async Task<Response<List<StudentCourseCountDTO>>> GetCourseCountsPerStudentAsync()
    {
        var result = await context.Students
            .Select(s => new StudentCourseCountDTO
            {
                StudentId = s.StudentId,
                FullName = $"{s.FirstName} {s.LastName}",
                CourseCount = context.Enrollments.Count(e => e.StudentId == s.StudentId)
            })
            .ToListAsync();

        return new Response<List<StudentCourseCountDTO>>(result);
    }

}
