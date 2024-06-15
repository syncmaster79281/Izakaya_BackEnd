using ISPAN.Izakaya.Entities;
using System.Collections.Generic;

namespace ISPAN.Izakaya.IDAL_IRepo_
{
    public interface IEmployeeRepository
    {
        void Create(EmployeeEntity entity);
        void Delete(int id);
        EmployeeEntity Get(int id);
        void Edit(EmployeeEntity entity);
        IEnumerable<EmployeeEntity> Search();
        EmployeeEntity GetEmployeeByName(string name);
        UserInfoEntity GetEmployeeByAccount(string account);
        List<EmployeeList> GetList();
    }
}
