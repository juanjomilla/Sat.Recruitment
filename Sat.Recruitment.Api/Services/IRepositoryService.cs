using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Services
{
    public interface IRepositoryService
    {
        IEnumerable<T> GetEntities<T>(Func<T, bool> predicate = null);

        Task CreateEntityAsync<T>(T entity);
    }
}
