using System;
using FWS.Generic.Framework.Helpers;
using FWS.Generic.Framework.UnitOfWork;

namespace FWS.Generic.Framework.Testing.Repository
{
    public class UnitOfWorkMock : IUnitOfWork
    {

        public CommitResult Commit(Action action)
        {
            try
            { 
                action();

                return new CommitResult();
            }
            catch (Exception e)
            {
                return new CommitResult(e);
            }
        }

        public void Dispose()
        {
            //Twiddle thumbs
        }
    }
}
