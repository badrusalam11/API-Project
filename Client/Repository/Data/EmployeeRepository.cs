using API.Models;
using Client.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Repository.Data
{
    public class EmployeeRepository : GeneralRepository<Employee, string>
    {
        public EmployeeRepository(Address address, string request = "Employees/") : base(address, request)
        {

        }
    }

}
