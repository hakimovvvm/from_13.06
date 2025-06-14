using Domain.ApiResponse;
using Domain.DTOs.StudentDTOs;
using Domain.Filters;

namespace Infrastructure.Interfaces;

public interface IStudentService
{
    Task<Response<StudentDTO>> GetByIdAsync(int id);
    Task<PagedResponse<List<StudentDTO>>> GetAllAsync(StudentFilter filter);
    Task<Response<string>> CreateAsync(CreateStudentDTO create);
    Task<Response<string>> UpdateAsync(StudentDTO update);
    Task<Response<string>> DeleteAsync(int id);

    Task<Response<List<StudentCourseCountDTO>>> GetStudentCourseCountsAsync();
    Task<Response<List<StudentDTO>>> GetStudentsWithoutCoursesAsync();
}
