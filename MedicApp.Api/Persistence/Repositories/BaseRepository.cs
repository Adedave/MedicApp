using MedicApp.Api.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicApp.Api.Persistence.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly MedicAppDbContext _context;

        public BaseRepository(MedicAppDbContext context)
        {
            _context = context;
        }
    }
}
