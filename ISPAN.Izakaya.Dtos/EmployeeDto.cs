using System;

namespace ISPAN.Izakaya.Dtos
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public int BranchId { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public DateTime HireDate { get; set; }
        public int Salary { get; set; }
        public string EmployeePassword { get; set; }
    }

}
