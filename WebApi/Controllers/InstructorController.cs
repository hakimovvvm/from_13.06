using Domain.ApiResponse;
using Domain.DTOs.InstructorDTOs;
using Domain.Filters;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InstructorController(IInstructorService instructorService)
{
    [HttpGet("{id}")]
    public async Task<Response<InstructorDTO>> GetByIdAsync(int id)
    {
        return await instructorService.GetByIdAsync(id);
    }

    [HttpGet]
    public async Task<PagedResponse<List<InstructorDTO>>> GetAllAsync([FromQuery] InstructorFilter filter)
    {
        return await instructorService.GetAllAsync(filter);
    }

    [HttpPost]
    public async Task<Response<string>> CreateAsync([FromBody] CreateInstructorDTO create)
    {
        return await instructorService.CreateAsync(create);
    }

    [HttpPut]
    public async Task<Response<string>> UpdateAsync([FromBody] InstructorDTO update)
    {
        return await instructorService.UpdateAsync(update);
    }

    [HttpDelete("{id}")]
    public async Task<Response<string>> DeleteAsync(int id)
    {
        return await instructorService.DeleteAsync(id);
    }

    [HttpGet("GetInstructorCourseCountsAsync")]
    public async Task<Response<List<InstructorCourseCountDTO>>> GetInstructorCourseCountsAsync()
    {
        return await instructorService.GetInstructorCourseCountsAsync();
    }
}
