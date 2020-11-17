using System;
using Microsoft.Extensions.Configuration;

namespace ECS.MemberManager.Core.DataAccess
{
    public static class DalFactory
    {
        private static Type _dalType;
        private static IConfigurationRoot _configurationRoot;

        static DalFactory()
        {
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile($"appsettings.json", optional: true, reloadOnChange: true);
            _configurationRoot    =  configurationBuilder.Build();
            
        }

        public static IDalManager GetManager()
        {
            if (_dalType == null)
            {
                var dalTypeName = _configurationRoot["DalManagerType"];

                if (!string.IsNullOrEmpty(dalTypeName))
                {
                    _dalType = Type.GetType(dalTypeName);
                }
                else
                    throw new NullReferenceException("DalManagerType");

                if (_dalType == null)
                    throw new ArgumentException(string.Format("Type {0} could not be found",dalTypeName));
            }

            return (IDalManager) Activator.CreateInstance(_dalType);
        }
    }
}