using API.Context;
using API.Models;
using API.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace API.Repository.Data
{
    public class EmployeeRepository : GeneralRepository<MyContext, Employee, string>
    {
        private readonly MyContext myContext;
        
        public EmployeeRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

    public int Register(RegisterVM registerVM)
        {
            //string formattedNIK = DateTime.Now.Year + "0" + (myContext.Employees.Count() + 1);
            int increment = myContext.Employees.ToList().Count;
            string formattedNIK = "";
            if (increment == 0)
            {
                formattedNIK = DateTime.Now.Year + "0" + increment.ToString();
            }
            else
            {
                string increment2 = myContext.Employees.ToList().Max(e => e.NIK);
                int formula = Int32.Parse(increment2) + 1;
                formattedNIK = formula.ToString();
            }

            int hasilCek = CheckAccount(registerVM);
            if (hasilCek != 1)
            {
                return hasilCek;
            }

            var employee = new Employee
            {
                NIK = formattedNIK,
                FirstName = registerVM.FirstName,
                LastName = registerVM.LastName,
                Phone = registerVM.Phone,
                BirthDate = registerVM.BirthDate,
                Salary = registerVM.Salary,
                Email = registerVM.Email,
                Gender = registerVM.Gender
            };
            myContext.Employees.Add(employee);
            myContext.SaveChanges();

            var account = new Account
            {
                NIK = employee.NIK,
                Password = BCrypt.Net.BCrypt.HashPassword(registerVM.Password)

            };
            myContext.Accounts.Add(account);
            myContext.SaveChanges();

            var accountRole = new AccountRole
            {
                NIK = employee.NIK,
                RoleId = 1
                
            };
            myContext.AccountRoles.Add(accountRole);
            myContext.SaveChanges();

            var education = new Education
            {
                Degree = registerVM.Degree,
                GPA = registerVM.GPA,
                UniversityId = registerVM.UniversityId
            };
            myContext.Educations.Add(education);
            myContext.SaveChanges();

           

            var profiling = new Profiling
            {
                NIK = employee.NIK,
                EducationId = education.EducationId
            };
            myContext.Profilings.Add(profiling);
            var result = myContext.SaveChanges();


            return result;
        }

        public  int CheckEmailPhone(Employee employee)
        {

            var data = myContext.Employees.Find(employee.NIK);
            var cekPhone = myContext.Employees.ToList().Where(e => e.Phone == employee.Phone);
            var cekEmail = myContext.Employees.ToList().Where(e => e.Email == employee.Email);


            if (data != null)
            {
                myContext.Entry(data).State = EntityState.Detached;
                foreach (var cp in cekPhone)
                {
                    if (cp.NIK != employee.NIK)
                    {
                        return 3;
                    }
                }
                foreach (var ce in cekEmail)
                {
                    if (ce.NIK != employee.NIK)
                    {
                        return 4;
                    }
                }

            }
            else
            {
                return 2;
            }
            return 1;
        }

        public int CheckInsert(Employee employee)
        {
            var data = myContext.Employees.SingleOrDefault(
               e => e.NIK == employee.NIK ||
               e.Phone == employee.Phone ||
               e.Email == employee.Email);
            if (data == null)
            {

                return 1;
            }
            else
            {
                if (data.NIK == employee.NIK)
                {
                    return -2;
                }
                else
                {
                    if (data.Phone == employee.Phone)
                    {
                        return -3;
                    }
                    else
                    {
                        if (data.Email == employee.Email)
                        {
                            return -4;
                        }
                        else { return 0; }
                    }

                }

            }
        }


        public int CheckAccount(RegisterVM registerVM)
        {
            var data = myContext.Employees.SingleOrDefault(
                e => e.NIK == registerVM.NIK ||
                e.Phone == registerVM.Phone ||
                e.Email == registerVM.Email);
            if (data == null)
            {

                return 1;
            }
            else
            {
                if (data.NIK == registerVM.NIK)
                {
                    return -2;
                }
                else
                {
                    if (data.Phone == registerVM.Phone)
                    {
                        return -3;
                    }
                    else
                    {
                        if (data.Email == registerVM.Email)
                        {
                            return -4;
                        }
                        else { return 0; }
                    }

                }

            }
        }

        public IEnumerable<Object> GetRegisterData()
        {
            var query = from employee in myContext.Employees
                        join account in myContext.Accounts
                            on employee.NIK equals account.NIK
                        join profiling in myContext.Profilings
                            on account.NIK equals profiling.NIK
                        join education in myContext.Educations
                            on profiling.EducationId equals education.EducationId
                        join university in myContext.Universities
                            on education.UniversityId equals university.UniversityId

                        //join accountrole in myContext.AccountRoles
                        //    on account.NIK equals accountrole.NIK
                        //join role in myContext.Roles
                        //    on accountrole.RoleId equals role.RoleId
                        select new
                        {
                            nik = employee.NIK,
                            firstName = employee.FirstName,
                            lastName = employee.LastName,
                            phone = employee.Phone,
                            email = employee.Email,
                            gender = employee.Gender == 0 ? "Male" : "Female",
                            birthDate = employee.BirthDate,
                            salary = employee.Salary,
                            //Education = new
                            //{
                            //    gpa = education.GPA,
                            //    degree = education.Degree,
                            //    University = new
                            //    {
                            //        name = university.Name
                            //    }
                            //},
                            GPA = education.GPA,
                            degree = education.Degree,
                            universityId = education.University.UniversityId,
                            universityName = education.University.Name,
                            roleName = myContext.AccountRoles.Where(accountrole => accountrole.NIK == employee.NIK).Select(accountrole => accountrole.Role.Name).ToList()
                        };
            return query;
            // lazy loading with query
        }

        //public Object GetRegisterData(string nik)
        //{
        //    var query = from employee in myContext.Employees
        //                join account in myContext.Accounts
        //                    on employee.NIK equals account.NIK
        //                join profiling in myContext.Profilings
        //                    on account.NIK equals profiling.NIK
        //                join education in myContext.Educations
        //                    on profiling.EducationId equals education.EducationId
        //                join university in myContext.Universities
        //                    on education.UniversityId equals university.UniversityId
        //                where employee.NIK == nik
        //                select new RegisterVM
        //                {
        //                    NIK = employee.NIK,
        //                    FirstName = employee.FirstName,
        //                    LastName = employee.LastName,
        //                    Phone = employee.Phone,
        //                    Email = employee.Email,
        //                    Gender = employee.Gender,
        //                    BirthDate = employee.BirthDate,
        //                    Salary = employee.Salary,
        //                    //Education = new
        //                    //{
        //                    //    gpa = education.GPA,
        //                    //    degree = education.Degree,
        //                    //    University = new
        //                    //    {
        //                    //        id = university.UniversityId,
        //                    //        name = university.Name
        //                    //    }
        //                    //},
        //                    GPA = education.GPA,
        //                    Degree = education.Degree,
        //                    UniversityId = education.University.UniversityId,
        //                    UniversityName = education.University.Name,
        //                    roleName = myContext.AccountRoles.Where(accountrole => accountrole.NIK == employee.NIK).Select(accountrole => accountrole.Role.Name).ToList()
        //                };
        //    return query;
        //    // lazy loading with query
        //}

        public RegisterVM GetRegisterData(string nik)
        {
            var query = myContext.Employees.Where(e => e.NIK == nik)
                .Include(e => e.Account)
                .ThenInclude(p => p.Profiling)
                .ThenInclude(e => e.Education)
                .ThenInclude(u => u.University)
                .FirstOrDefault();
            if (query == null)
            {
                return null;
            }
            var selectedData = new RegisterVM
            {
                NIK = query.NIK,
                FirstName = query.FirstName,
                LastName = query.LastName,
                Phone = query.Phone,
                Email = query.Email,
                Gender = query.Gender,
                BirthDate = query.BirthDate,
                Salary = query.Salary,
                GPA = query.Account.Profiling.Education.GPA,
                Degree = query.Account.Profiling.Education.Degree,
                UniversityId = query.Account.Profiling.Education.University.UniversityId,
                UniversityName = query.Account.Profiling.Education.University.Name,
                RoleName = myContext.AccountRoles.Where(accountrole => accountrole.NIK == query.NIK).Select(accountrole => accountrole.Role.Name).ToList()

            };
            return selectedData;

        }


        public int UpdateRegisterData(RegisterVM registerVM)
        {
            int hasilCek = CheckUpdateRegister(registerVM);
            if (hasilCek != 1)
            {
                return hasilCek;
            }
           
            var employee = new Employee
            {
                NIK = registerVM.NIK,
                FirstName = registerVM.FirstName,
                LastName = registerVM.LastName,
                Phone = registerVM.Phone,
                BirthDate = registerVM.BirthDate,
                Salary = registerVM.Salary,
                Email = registerVM.Email,
                Gender = registerVM.Gender
            };
            myContext.Entry(employee).State = EntityState.Modified;
            myContext.SaveChanges();

            var findEducationId = myContext.Profilings.Find(registerVM.NIK);

            var education = new Education
            {
                EducationId = findEducationId.EducationId,
                Degree = registerVM.Degree,
                GPA = registerVM.GPA,
                UniversityId = registerVM.UniversityId
            };
            myContext.Entry(findEducationId).State = EntityState.Detached;
            myContext.Entry(education).State = EntityState.Modified;
            var result = myContext.SaveChanges();
            return result;
        }

        public int CheckUpdateRegister(RegisterVM registerVM)
        {

            var data = myContext.Employees.Find(registerVM.NIK);
            var cekPhone = myContext.Employees.ToList().Where(e => e.Phone == registerVM.Phone);
            var cekEmail = myContext.Employees.ToList().Where(e => e.Email == registerVM.Email);

            myContext.Entry(data).State = EntityState.Detached;

            if (data != null)
            {
                //myContext.Entry(data).State = EntityState.Detached;
                foreach (var cp in cekPhone)
                {
                    if (cp.NIK != registerVM.NIK)
                    {
                        return -3;
                    }
                }
                foreach (var ce in cekEmail)
                {
                    if (ce.NIK != registerVM.NIK)
                    {
                        return -4;
                    }
                }

            }
            else
            {
                return -2;
            }
            return 1;
        }

        public int DeleteRegisterData(string NIK)
        {
            var profiling = myContext.Profilings.Where(p => p.NIK == NIK).FirstOrDefault();
            var education = myContext.Educations.Where(e => e.EducationId == profiling.EducationId).FirstOrDefault();
            myContext.Remove(education);
            var respond = myContext.SaveChanges();
            return respond;
        }



    }
}
