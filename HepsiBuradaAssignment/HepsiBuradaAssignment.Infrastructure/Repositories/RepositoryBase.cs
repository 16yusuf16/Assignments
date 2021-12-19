using System;
using HepsiBuradaAssignment.Domain.Interfaces;
using HepsiBuradaAssignment.Infrastructure.Data.Context;

namespace HepsiBuradaAssignment.Infrastructure.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : IEfEntity
    {
        private readonly AssignmentContext _assignmentContext;
        public RepositoryBase(AssignmentContext assignmentContext)
        {
            _assignmentContext = assignmentContext;
        }

        public int Save() => _assignmentContext.SaveChangesAsync().Result;

        public void Add(T entity) => _assignmentContext.Add(entity);

        public void Update(T entity) => _assignmentContext.Update(entity);

        public void Dispose()
        {
            _assignmentContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
