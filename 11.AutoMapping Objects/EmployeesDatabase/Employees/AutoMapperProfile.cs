namespace Employees.Client
{
    using AutoMapper;
    using Employees.DtoModels;
    using Employees.Models;

    class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeDto, Employee>();
            CreateMap<Employee, ManagerDto>()
                .ForMember(managerDto => managerDto.ManagedEmployeesCount, a=>a.MapFrom(employee => employee.ManagedEmployees.Count));
            CreateMap<Employee, EmployeePersonalDto>();
            CreateMap<Employee, EmployeeManagerDto>()
                .ForMember(empl => empl.EmployeeFirstName, a => a.MapFrom(emp => emp.FirstName))
                .ForMember(empl => empl.EmployeeLastName, a => a.MapFrom(emp => emp.LastName))
                .ForMember(e=>e.EmployeeSalary,a=>a.MapFrom(s=>s.Salary))
                .ForMember(m => m.ManagerLastName, a => a.MapFrom(m => m.Manager.LastName));
        }
    }
}
