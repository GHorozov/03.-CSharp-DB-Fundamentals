namespace Employees.Application
{
    using System;
    using AutoMapper;
    using Employees.Models;
    using Employees.DtoModels;
    using AutoMapper.QueryableExtensions;


    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeDto, Employee>();
            CreateMap<Employee, EmployeePersonalDto>();
            CreateMap<Employee, ManagerDto>();
            CreateMap<ManagerDto, Employee>();
            CreateMap<Employee, EmployeeOlderThanDto>();
            CreateMap<EmployeeOlderThanDto, Employee>();
        }
    }
}
