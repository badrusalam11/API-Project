using API.Context;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class RoleRepository : GeneralRepository<MyContext, AccountRole, int>
    {
        private readonly MyContext myContext;
        public RoleRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        public int AssignManager(AccountRole accountRole)
        {
            var emp = myContext.Employees.Find(accountRole.NIK);
            if (emp == null)
            {
                return 0;
            }
            var acr = myContext.AccountRoles.ToList().Where(acr => acr.NIK == accountRole.NIK);
            foreach (var a in acr)
            {
                if (a.RoleId == 2)
                {
                    return -1;
                }
            }
            var acrs = new AccountRole
            {
                NIK = accountRole.NIK,
                RoleId = 2

            };
            myContext.AccountRoles.Add(acrs);
            int result = myContext.SaveChanges();
            return result;
        }

    }
}
