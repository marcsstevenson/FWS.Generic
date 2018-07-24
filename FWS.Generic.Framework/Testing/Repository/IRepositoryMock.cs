using System.Collections.Generic;

namespace FWS.Generic.Framework.Testing.Repository
{
    public interface IRepositoryMock
    {
        void SetData<T>(IList<T> dbSet) where T : class;
        IList<T> GetData<T>() where T : class;
    }
}