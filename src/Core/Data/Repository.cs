using System.Collections.Generic;
using System.Linq;
using CommunitySite.Core.Domain;

namespace CommunitySite.Core.Data
{
    public interface Repository
    {
        void Save<T>(T item);
        IQueryable<T> All<T>();
        //Member GetByEmail(string emailAddress);
    }
}