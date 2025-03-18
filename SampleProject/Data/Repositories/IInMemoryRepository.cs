using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public interface IInMemoryRepository<T> : IRepository<T> where T : IdObject
    {
    }
}