using application.DTOs;
using AutoMapper;
using domain;

namespace application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Entidad → DTO de salida
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.FullName,
                           opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.DepartmentName,
                           opt => opt.MapFrom(src => src.Department.Name));

            // DTO de entrada → Entidad
            CreateMap<CreateEmployeeDto, Employee>();
        }
    }
}
