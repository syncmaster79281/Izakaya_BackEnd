using ISPAN.Izakaya.Dtos;
using ISPAN.Izakaya.Entities;
using ISPAN.Izakaya.IDAL_IRepo_;
using System.Collections.Generic;
using System.Linq;

namespace ISPAN.Izakaya.BLL_Service_
{

    public class EmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public IEnumerable<EmployeeDto> Search()
        {
            return _employeeRepository.Search().Select(x => x.ToDto());
        }
        public EmployeeDto Get(int id)
        {
            return _employeeRepository.Get(id).ToDto();
        }
        public void Create(EmployeeDto dto)
        {
            var entity = dto.ToEntity();
            _employeeRepository.Create(entity);
        }
        public void Edit(EmployeeDto dto)
        {
            var entity = dto.ToEntity();
            _employeeRepository.Edit(entity);
        }
        public void Delete(int id)
        {
            _employeeRepository.Delete(id);
        }
        public EmployeeDto GetEmployeeByName(string name)
        {
            return _employeeRepository.GetEmployeeByName(name).ToDto();
        }
        public List<EmployeeList> GetList()
        {
            return _employeeRepository.GetList();
        }
    }
}
