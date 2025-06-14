using AutoMapper;
using Domain.DTOs.CourseDTOs;
using Domain.DTOs.CurseDTOs;
using Domain.DTOs.InstructorDTOs;
using Domain.DTOs.StudentDTOs;
using Domain.Entites;

namespace Infrastructure.AutoMapper;

public class InfrastructureProfile : Profile
{
    public InfrastructureProfile()
    {
        CreateMap<Student, StudentDTO>().ReverseMap();
        CreateMap<CreateStudentDTO, Student>().ReverseMap();
        CreateMap<UpdateStudentDTO, Student>().ReverseMap();
        CreateMap<StudentCourseCountDTO, Student>().ReverseMap();

        CreateMap<Course, CourseDTO>().ReverseMap();
        CreateMap<CreateCourseDTO, Course>().ReverseMap();
        CreateMap<UpdateCourseDTO, Course>().ReverseMap();
        CreateMap<CourseAverageGradeDTO, Course>().ReverseMap();
        CreateMap<CourseStudentCountDTO, Course>().ReverseMap();

        CreateMap<Instructor, InstructorDTO>().ReverseMap();
        CreateMap<CreateInstructorDTO, Instructor>().ReverseMap();
        CreateMap<UpdateInstructorDTO, Instructor>().ReverseMap();
        CreateMap<InstructorCourseCountDTO, Instructor>().ReverseMap();
    }
}
