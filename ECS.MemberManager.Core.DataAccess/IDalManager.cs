using System;

namespace ECS.MemberManager.Core.DataAccess
{
    public interface IDalManager : IDisposable
    {
        T GetProvider<T>() where T : class;
    }
}