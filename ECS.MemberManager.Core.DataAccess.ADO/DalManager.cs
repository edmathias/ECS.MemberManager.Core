using System;
using System.IO;
using Csla.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ECS.MemberManager.Core.DataAccess.ADO
{
    public class DalManager : IDalManager
    {
        private static readonly string _typeMask = typeof(DalManager).FullName.Replace("DalManager", @"{0}");

         public T GetProvider<T>() where T : class
                {
                    var typeName = string.Format(_typeMask, typeof(T).Name.Substring(1));

                    var type = Type.GetType(typeName);
                    if (type != null)
                        return Activator.CreateInstance(type) as T;
                    else
                     throw new NotImplementedException(typeName);
                }

         public void Dispose()
         {
         }
    }
}
