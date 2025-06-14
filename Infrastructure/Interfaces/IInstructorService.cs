using Domain.ApiResponse;
using Domain.DTOs.InstructorDTOs;
using Domain.Filters;

namespace Infrastructure.Interfaces;

public interface IInstructorService
{
    Task<Response<InstructorDTO>> GetByIdAsync(int id);
    Task<PagedResponse<List<InstructorDTO>>> GetAllAsync(InstructorFilter filter);
    Task<Response<string>> CreateAsync(CreateInstructorDTO create);
    Task<Response<string>> UpdateAsync(InstructorDTO update);
    Task<Response<string>> DeleteAsync(int id);

    Task<Response<List<InstructorCourseCountDTO>>> GetInstructorCourseCountsAsync();
}
