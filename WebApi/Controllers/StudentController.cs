using Domain.ApiResponse;
using Domain.DTOs.StudentDTOs;
using Domain.Filters;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentController(IStudentService studentService)
{
    [HttpGet("{id}")]
    public async Task<Response<StudentDTO>> GetByIdAsync(int id)
    {
        return await studentService.GetByIdAsync(id);
    }

    [HttpGet]
    public async Task<PagedResponse<List<StudentDTO>>> GetAllAsync([FromQuery] StudentFilter filter)
    {
        return await studentService.GetAllAsync(filter);
    }

    [HttpPost]
    public async Task<Response<string>> CreateAsync([FromBody] CreateStudentDTO create)
    {
        return await studentService.CreateAsync(create);
    }

    [HttpPut]
    public async Task<Response<string>> UpdateAsync([FromBody] StudentDTO update)
    {
        return await studentService.UpdateAsync(update);
    }

    [HttpDelete("{id}")]
    public async Task<Response<string>> DeleteAsync(int id)
    {
        return await studentService.DeleteAsync(id);
    }

    [HttpGet("GetStudentCourseCountsAsync")]
    public async Task<Response<List<StudentCourseCountDTO>>> GetStudentCourseCountsAsync()
    {
        return await studentService.GetStudentCourseCountsAsync();
    }

    [HttpGet("GetStudentsWithoutCoursesAsync")]
    public async Task<Response<List<StudentDTO>>> GetStudentsWithoutCoursesAsync()
    {
        return await studentService.GetStudentsWithoutCoursesAsync();
    }
}
