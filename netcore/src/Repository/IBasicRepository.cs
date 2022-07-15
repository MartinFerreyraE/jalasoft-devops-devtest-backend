using System.Collections.Generic;

namespace Contacts.Repository
{ 
    public interface BasicRepository<T, ID> 
    {
        T Save(T entity);

        T FindById(ID id);        

        bool ExistsById(ID id);

        IEnumerable<T> FindAll();
    }
}