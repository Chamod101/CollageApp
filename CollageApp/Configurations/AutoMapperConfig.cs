using AutoMapper;
using CollageApp.Data;
using CollageApp.Models;

namespace CollageApp.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig() 
        { 
            CreateMap<Student,StudentDTO>().ReverseMap();
        }
    }
}
