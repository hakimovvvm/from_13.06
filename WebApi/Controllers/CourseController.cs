using Domain.ApiResponse;
using Domain.DTOs.CourseDTOs;
using Domain.DTOs.CurseDTOs;
using Domain.Filters;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CourseController(IСourseService сourseService)
{
    [HttpGet("{id}")]
    public async Task<Response<CourseDTO>> GetByIdAsync(int id)
    {
        return await сourseService.GetByIdAsync(id);
    }
    [HttpGet]
    public async Task<PagedResponse<List<CourseDTO>>> GetAllAsync([FromQuery]CourseFilter filter)
    {
        return await сourseService.GetAllAsync(filter);
    }
    [HttpPost]
    public async Task<Response<string>> CreateAsync(CreateCourseDTO create)
    {
        return await сourseService.CreateAsync(create);
    }
    [HttpPut]
    public async Task<Response<string>> UpdateAsync(CourseDTO update)
    {
        return await сourseService.UpdateAsync(update);
    }
    [HttpDelete("{id}")]
    public async Task<Response<string>> DeleteAsync(int id)
    {
        return await сourseService.DeleteAsync(id);
    }
    [HttpGet("GetCourseStudentCountsAsync")]
    public async Task<Response<List<CourseStudentCountDTO>>> GetCourseStudentCountsAsync()
    {
        return await сourseService.GetCourseStudentCountsAsync();
    }
    [HttpGet("GetCourseAverageGradesAsync")]
    public async Task<Response<List<CourseAverageGradeDTO>>> GetCourseAverageGradesAsync()
    {
        return await сourseService.GetCourseAverageGradesAsync();
    }
}
