using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ToDo.ApplicationDBContext;
using ToDo.DataAccess.Repository.IRepository;

namespace ToDo.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public IToDoRepository ToDoRepository { get; private set; }

        public IAudioRepository audioRepository { get; private set; }

        public IPhotoRepository photoRepository { get; private set; }

        public IApplicationUser applicationUser { get; private set; }

        private readonly AppDbCon _db;

        public UnitOfWork(AppDbCon db)
        {
            _db = db;
            ToDoRepository = new ToDoRepository(_db);
            audioRepository = new AudioRepository(_db);
            photoRepository = new PhotoRepository(_db);
            applicationUser = new ApplicationUserRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
