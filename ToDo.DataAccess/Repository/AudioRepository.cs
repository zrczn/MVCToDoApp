using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ToDo.ApplicationDBContext;
using ToDo.DataAccess.Repository.IRepository;
using ToDo.Models;

namespace ToDo.DataAccess.Repository
{
    internal class AudioRepository : Repository<AudioModel>, IAudioRepository
    {
        private readonly AppDbCon _db;

        public AudioRepository(AppDbCon db) : base(db)
        {
            _db = db;
        }

        public void Update(AudioModel obj)
        {
            _db.Update(obj);
        }
    }
}
