using API.Context;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class UniversityRepository : GeneralRepository<MyContext, University, int>
    {
        private readonly MyContext myContext;
        public UniversityRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        
        public IEnumerable<Object> UniversityCount()
        {
            var list = from edu in myContext.Educations
                       join uni in myContext.Universities on edu.UniversityId equals uni.UniversityId
                       group uni by new { edu.UniversityId, uni.Name } into Group
                       select new
                       {
                           UniversityId = Group.Key.UniversityId,
                           UniversityName = Group.Key.Name,
                           Count = Group.Count()
                       };
            return list.ToList();
        }

    }
}
