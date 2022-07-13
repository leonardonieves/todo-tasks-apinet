using APICore.Data.Entities;
using APICore.Data.Repository;
using System;
using System.Threading.Tasks;

namespace APICore.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CoreDbContext _context;

        private bool disposed = false;

        public UnitOfWork(CoreDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            TaskMRepository = TaskMRepository ?? new GenericRepository<TaskM>(_context);
        }

        public IGenericRepository<TaskM> TaskMRepository { get; set; }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public int Save()
        {
            throw new NotImplementedException();
        }

        protected void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                this.disposed = true;
            }
        }
    }
}