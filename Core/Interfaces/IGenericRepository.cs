using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Specifications;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T: EntityBase
    {
         Task<T> GetByIdAsync(int id);
         Task<List<T>> GetAllListAsync();

         Task<T> GetEntityBySpec(ISpecification<T> spec);

         Task<IReadOnlyList<T>> GetAllListAsync(ISpecification<T> spec);

         Task<int> CountAsync(ISpecification<T> spec);
    }
}