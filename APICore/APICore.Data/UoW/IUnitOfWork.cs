using APICore.Data.Entities;
using APICore.Data.Repository;
using System;
using System.Threading.Tasks;

namespace APICore.Data.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TaskM> TaskMRepository { get; set; }

        int Save();

        Task<int> CommitAsync();
    }
}