using System.Runtime.InteropServices;
using AutoMapper;
using PRN232PRJ.ViewModel;
using BusinessObject.Models;

namespace PRN232PRJ.AutoMapper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper() 
        {
            CreateMap<Employee, EmployeeVM>()
                //.ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.HasValue ? src.CreatedAt.Value : DateTime.Now))
                .ReverseMap();
            CreateMap<Department, DepartmentVM>()
                .ReverseMap();
            CreateMap<Salary, SalaryVM>()
                .ReverseMap();
        }
    }
}
