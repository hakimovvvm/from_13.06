using System.Net;
using AutoMapper;
using Domain.ApiResponse;
using Domain.DTOs.CourseDTOs;
using Domain.DTOs.CurseDTOs;
using Domain.Entites;
using Domain.Filters;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class CourseService(DataContext context, IMapper mapper) : IСourseService
{
    public async Task<Response<CourseDTO>> GetByIdAsync(int id)
    {
        var course = await context.Courses.FindAsync(id);
        if (course == null)
            return new Response<CourseDTO>(HttpStatusCode.NotFound, "Course not found");

        var dto = mapper.Map<CourseDTO>(course);
        return new Response<CourseDTO>(dto);
    }

    public async Task<PagedResponse<List<CourseDTO>>> GetAllAsync(CourseFilter filter)
    {
        var query = context.Courses.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.Title))
            query = query.Where(c => c.Title.ToLower().Contains(filter.Title.ToLower()));

        if (filter.MinPrice.HasValue)
            query = query.Where(c => c.Price >= filter.MinPrice.Value);

        if (filter.MaxPrice.HasValue)
            query = query.Where(c => c.Price <= filter.MaxPrice.Value);

        int totalResults = await query.CountAsync();

        var courses = await query
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();

        var dtos = mapper.Map<List<CourseDTO>>(courses);
        var totalPages = (int)Math.Ceiling(totalResults / (double)filter.PageSize);

        return new PagedResponse<List<CourseDTO>>(dtos, totalPages, filter.PageNumber, filter.PageSize);
    }

    public async Task<Response<string>> CreateAsync(CreateCourseDTO create)
    {
        if (string.IsNullOrWhiteSpace(create.Title))
            return new Response<string>(HttpStatusCode.BadRequest, "Title is required");

        if (create.Price < 0)
            return new Response<string>(HttpStatusCode.BadRequest, "Price cannot be negative");

        var course = mapper.Map<Course>(create);

        context.Courses.Add(course);
        await context.SaveChangesAsync();

        return new Response<string>(null!, "Course created successfully");
    }

    public async Task<Response<string>> UpdateAsync(CourseDTO update)
    {
        var course = await context.Courses.FindAsync(update.CourseId);
        if (course == null)
            return new Response<string>(HttpStatusCode.NotFound, "Course not found");

        if (string.IsNullOrWhiteSpace(update.Title))
            return new Response<string>(HttpStatusCode.BadRequest, "Title is required");

        if (update.Price < 0)
            return new Response<string>(HttpStatusCode.BadRequest, "Price cannot be negative");

        mapper.Map(update, course);
        await context.SaveChangesAsync();

        return new Response<string>(null!, "Course updated successfully");
    }

    public async Task<Response<string>> DeleteAsync(int id)
    {
        var course = await context.Courses.FindAsync(id);
        if (course == null)
            return new Response<string>(HttpStatusCode.NotFound, "Course not found");

        context.Courses.Remove(course);
        await context.SaveChangesAsync();

        return new Response<string>(null!, "Course deleted successfully");
    }

    public async Task<Response<List<CourseStudentCountDTO>>> GetCourseStudentCountsAsync()
    {
        var data = await context.Courses
            .Select(c => new CourseStudentCountDTO
            {
                CourseId = c.CourseId,
                Title = c.Title,
                StudentCount = c.Enrollments.Count
            })
            .ToListAsync();

        return new Response<List<CourseStudentCountDTO>>(data);
    }

    public async Task<Response<List<CourseAverageGradeDTO>>> GetCourseAverageGradesAsync()
    {
        var data = await context.Courses
            .Select(c => new CourseAverageGradeDTO
            {
                CourseId = c.CourseId,
                Title = c.Title,
                AverageGrade = (int)c.Enrollments.Average(e => e.Grade)
            })
            .ToListAsync();

        return new Response<List<CourseAverageGradeDTO>>(data);
    }
}
