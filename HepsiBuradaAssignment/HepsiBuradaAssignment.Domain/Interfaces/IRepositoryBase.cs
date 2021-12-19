using System;
namespace HepsiBuradaAssignment.Domain.Interfaces
{
    public interface IRepositoryBase<in T> : IDisposable where T : IEfEntity
    {
        int Save();
        void Add(T entity);
        void Update(T entity);
    }
}
