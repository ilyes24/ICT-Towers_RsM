using RelationShipManager.Dtos;
using RelationShipManager.Entities;
using AutoMapper;

namespace RelationShipManager.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeDto, Employee>();

            CreateMap<UserDto,MyUser>();
            CreateMap<MyUser, UserDto>();

            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.Category,
                            opts => opts.MapFrom(src => src.Category1));
                
            CreateMap<CategoryDto, Category>()
                .ForMember(dest => dest.Category1,
                            opts => opts.MapFrom(src => src.Category));;
        }
    }
}
