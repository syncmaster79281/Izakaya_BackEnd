using ISPAN.Izakaya.Entities;
using ISPAN.Izakaya.IDAL_IRepo_;

namespace Izakayamvc.ViewModels.Permissions
{
    public class PermissionsSetting
    {
        private readonly IEmployeeRepository _repo;
        public int BranchId { get; }
        public string Name { get; }
        public string Role { get; }
        public int EmployeeId { get; }
        public PermissionsSetting(IEmployeeRepository repo, string name)
        {
            _repo = repo;
            UserInfoEntity data = _repo.GetEmployeeByAccount(name);
            BranchId = data.BranchId;
            Name = data.Name;
            Role = data.Role;
            EmployeeId = data.Id;
        }
    }
}