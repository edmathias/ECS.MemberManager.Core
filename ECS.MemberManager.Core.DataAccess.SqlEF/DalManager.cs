using System;

namespace ECS.MemberManager.Core.DataAccess.SqlEF
{
    public class DalManager : IDalManager
    {
            
        private static readonly string _typeMask = typeof(DalManager).FullName.Replace("DalManager", @"{0}");

        public void Dispose()
        {
        }

         public T GetProvider<T>() where T : class
                {
                    var typeName = string.Format(_typeMask, typeof(T).Name.Substring(1));

                    var type = Type.GetType(typeName);
                    if (type != null)
                        return Activator.CreateInstance(type) as T;
                    else
                     throw new NotImplementedException(typeName);
                }
        
    }
}
