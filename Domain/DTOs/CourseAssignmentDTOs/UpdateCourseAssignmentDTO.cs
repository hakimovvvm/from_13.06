using Domain.DTOs.CurseAssignmentDTOs;

namespace Domain.DTOs.CourseAssignmentDTOs;

public class UpdateCourseAssignmentDTO : CreateCourseAssignmentDTO
{
    public int CourseAssignmentId { get; set; }
}

