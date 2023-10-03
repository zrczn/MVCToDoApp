using Microsoft.EntityFrameworkCore.Query.Internal;
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
    public class PhotoRepository : Repository<PhotoModel>, IPhotoRepository
    {
        private readonly AppDbCon _db;

        public PhotoRepository(AppDbCon db): base(db)
        {
            _db = db;
        }

        public void Update(PhotoModel obj)
        {
            _db.Update(obj);
        }
    }
}
