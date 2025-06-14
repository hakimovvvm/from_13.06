using Domain.ApiResponse;
using Domain.DTOs.CourseDTOs;
using Domain.DTOs.EnrollmentDTOs;
using Domain.DTOs.StudentDTOs;
using Domain.Filters;

namespace Infrastructure.Interfaces;

public interface IEnrollmentService
{
    Task<Response<EnrollmentDTO>> GetByIdAsync(int id);
    Task<Response<string>> CreateAsync(CreateEnrollmentDTO create);
    Task<Response<string>> UpdateAsync(EnrollmentDTO update);
    Task<Response<string>> DeleteAsync(int id);
    Task<Response<List<StudentDTO>>> GetStudentsWithoutCoursesAsync();
    Task<Response<List<CourseStudentCountDTO>>> GetStudentCountsPerCourseAsync();
    Task<Response<List<CourseAverageGradeDTO>>> GetAverageGradesPerCourseAsync();
    Task<Response<List<StudentCourseCountDTO>>> GetCourseCountsPerStudentAsync();
    Task<PagedResponse<List<EnrollmentDTO>>> GetAllAsync(EnrollmentFilter filter);

}
