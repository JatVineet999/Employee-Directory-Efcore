using AutoMapper;
using Infrastructure.Models;
using Application.DTO;

namespace Application.Utility
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDto>()
             .ForMember(dest => dest.JobTitle, opt => opt.MapFrom(src => src.Role != null ? src.Role.RoleName : null))
             .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department != null ? src.Department.DepartmentName : null));

            CreateMap<AddEmployeeDto, Employee>();
            CreateMap<Employee, AddEmployeeDto>()
               .IncludeBase<Employee, EmployeeDto>();

            CreateMap<Department, DepartmentDto>();
            CreateMap<Role, RoleDto>();

            CreateMap<(Department, List<Role>), DepartmentAndRolesDto>()
             .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Item1))
             .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Item2));
        }
    }
}
