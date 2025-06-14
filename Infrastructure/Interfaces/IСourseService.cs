using Domain.ApiResponse;
using Domain.DTOs.CourseDTOs;
using Domain.DTOs.CurseDTOs;
using Domain.Filters;

namespace Infrastructure.Interfaces;

public interface IСourseService
{
    Task<Response<CourseDTO>> GetByIdAsync(int id);
    Task<PagedResponse<List<CourseDTO>>> GetAllAsync(CourseFilter filter);
    Task<Response<string>> CreateAsync(CreateCourseDTO create);
    Task<Response<string>> UpdateAsync(CourseDTO update);
    Task<Response<string>> DeleteAsync(int id);

    Task<Response<List<CourseStudentCountDTO>>> GetCourseStudentCountsAsync();
    Task<Response<List<CourseAverageGradeDTO>>> GetCourseAverageGradesAsync();
}
