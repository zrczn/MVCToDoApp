using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork 
    {
        IToDoRepository ToDoRepository { get; }
        IAudioRepository audioRepository { get; }
        IPhotoRepository photoRepository { get; }
        IApplicationUser applicationUser { get; }
        void Save();
    }
}
