using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Interface
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> Get(); // get semua data
        // bisa juga pake IList, cocok untuk manipulasi tabel
        // kalo cuma read data, lebih cocok IEnumerable
        Employee Get(string NIK); //get 1 row data berdasarkan NIK
        int Insert(Employee employee);
        int Update(string NIK, Employee employee);
        int Delete(string NIK);
    }
}
