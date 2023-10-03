using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.ApplicationDBContext;
using ToDo.DataAccess.Repository.IRepository;
using ToDo.Models;

namespace ToDo.DataAccess.Repository
{
    internal class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUser
    {
        private readonly AppDbCon _db;

        public ApplicationUserRepository(AppDbCon db) : base(db)
        {
            _db = db;
        }

        public void Update(ApplicationUser entity)
        {
            _db.applicationUsers.Update(entity);
        }
    }
}
