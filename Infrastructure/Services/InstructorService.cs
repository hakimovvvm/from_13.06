using System.Net;
using AutoMapper;
using Domain.ApiResponse;
using Domain.DTOs.InstructorDTOs;
using Domain.Entites;
using Domain.Filters;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class InstructorService(DataContext context, IMapper mapper) : IInstructorService
{
    public async Task<Response<InstructorDTO>> GetByIdAsync(int id)
    {
        var instructor = await context.Instructors.FindAsync(id);
        if (instructor == null)
            return new Response<InstructorDTO>(HttpStatusCode.NotFound, "Instructor not found");

        var dto = mapper.Map<InstructorDTO>(instructor);
        return new Response<InstructorDTO>(dto);
    }

    public async Task<PagedResponse<List<InstructorDTO>>> GetAllAsync(InstructorFilter filter)
    {
        var query = context.Instructors.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.FirstName))
            query = query.Where(i => i.FirstName.ToLower().Contains(filter.FirstName.ToLower()));

        if (!string.IsNullOrWhiteSpace(filter.LastName))
            query = query.Where(i => i.LastName.ToLower().Contains(filter.LastName.ToLower()));

        int totalResults = await query.CountAsync();

        var instructors = await query
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();

        var dtos = mapper.Map<List<InstructorDTO>>(instructors);
        var totalPages = (int)Math.Ceiling(totalResults / (double)filter.PageSize);

        return new PagedResponse<List<InstructorDTO>>(dtos, totalPages, filter.PageNumber, filter.PageSize);
    }

    public async Task<Response<string>> CreateAsync(CreateInstructorDTO create)
    {
        if (string.IsNullOrWhiteSpace(create.FirstName) || string.IsNullOrWhiteSpace(create.LastName))
            return new Response<string>(HttpStatusCode.BadRequest, "First and Last Name are required");

        var instructor = mapper.Map<Instructor>(create);
        context.Instructors.Add(instructor);
        await context.SaveChangesAsync();

        return new Response<string>(null!, "Instructor created successfully");
    }

    public async Task<Response<string>> UpdateAsync(InstructorDTO update)
    {
        var instructor = await context.Instructors.FindAsync(update.InstructorId);
        if (instructor == null)
            return new Response<string>(HttpStatusCode.NotFound, "Instructor not found");

        if (string.IsNullOrWhiteSpace(update.FirstName) || string.IsNullOrWhiteSpace(update.LastName))
            return new Response<string>(HttpStatusCode.BadRequest, "First and Last Name are required");

        mapper.Map(update, instructor);
        await context.SaveChangesAsync();

        return new Response<string>(null!, "Instructor updated successfully");
    }

    public async Task<Response<string>> DeleteAsync(int id)
    {
        var instructor = await context.Instructors.FindAsync(id);
        if (instructor == null)
            return new Response<string>(HttpStatusCode.NotFound, "Instructor not found");

        context.Instructors.Remove(instructor);
        await context.SaveChangesAsync();

        return new Response<string>(null!, "Instructor deleted successfully");
    }

    public async Task<Response<List<InstructorCourseCountDTO>>> GetInstructorCourseCountsAsync()
    {
        var data = await context.Instructors
            .Select(i => new InstructorCourseCountDTO
            {
                InstructorId = i.InstructorId,
                FullName = $"{i.FirstName} {i.LastName}",
                CourseCount = context.CourseAssignments.Count(ca => ca.InstructorId == i.InstructorId)
            })
            .ToListAsync();

        return new Response<List<InstructorCourseCountDTO>>(data);
    }
}
