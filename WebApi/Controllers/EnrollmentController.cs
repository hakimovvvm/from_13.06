using Domain.ApiResponse;
using Domain.DTOs.CourseDTOs;
using Domain.DTOs.EnrollmentDTOs;
using Domain.DTOs.StudentDTOs;
using Domain.Filters;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EnrollmentController(IEnrollmentService enrollmentService)
{
    [HttpGet("{id}")]
    public async Task<Response<EnrollmentDTO>> GetByIdAsync(int id)
    {
        return await enrollmentService.GetByIdAsync(id);
    }

    [HttpGet]
    public async Task<PagedResponse<List<EnrollmentDTO>>> GetAllAsync(EnrollmentFilter filter)
    {
        return await enrollmentService.GetAllAsync(filter);
    }

    [HttpPost]
    public async Task<Response<string>> CreateAsync([FromBody] CreateEnrollmentDTO create)
    {
        return await enrollmentService.CreateAsync(create);
    }

    [HttpPut]
    public async Task<Response<string>> UpdateAsync([FromBody] EnrollmentDTO update)
    {
        return await enrollmentService.UpdateAsync(update);
    }

    [HttpDelete("{id}")]
    public async Task<Response<string>> DeleteAsync(int id)
    {
        return await enrollmentService.DeleteAsync(id);
    }

    [HttpGet("GetStudentsWithoutCoursesAsync")]
    public async Task<Response<List<StudentDTO>>> GetStudentsWithoutCoursesAsync()
    {
        return await enrollmentService.GetStudentsWithoutCoursesAsync();
    }

    [HttpGet("GetStudentCountsPerCourseAsync")]
    public async Task<Response<List<CourseStudentCountDTO>>> GetStudentCountsPerCourseAsync()
    {
        return await enrollmentService.GetStudentCountsPerCourseAsync();
    }

    [HttpGet("GetAverageGradesPerCourseAsync")]
    public async Task<Response<List<CourseAverageGradeDTO>>> GetAverageGradesPerCourseAsync()
    {
        return await enrollmentService.GetAverageGradesPerCourseAsync();
    }

    [HttpGet("GetCourseCountsPerStudentAsync")]
    public async Task<Response<List<StudentCourseCountDTO>>> GetCourseCountsPerStudentAsync()
    {
        return await enrollmentService.GetCourseCountsPerStudentAsync();
    }
}
