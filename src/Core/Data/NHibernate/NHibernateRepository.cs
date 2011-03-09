using System;
using System.Linq;
using CommunitySite.Core.Domain;
using NHibernate;

namespace CommunitySite.Core.Data.NHibernate
{
    public class NHibernateRepository : Repository
    {
        readonly ISessionFactory _sessionFactory;

        public NHibernateRepository(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }


        public void Save<T>(T item)
        {
            using(var session = _sessionFactory.OpenSession())
            {
                session.SaveOrUpdate(item);
            }
        }

        public IQueryable<T> All<T>()
        {
            throw new NotImplementedException();
        }

        public Member GetByEmail(string emailAddress)
        {
            throw new NotImplementedException();
        }
    }
}