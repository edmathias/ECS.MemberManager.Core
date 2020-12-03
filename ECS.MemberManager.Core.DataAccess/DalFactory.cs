using System;
using Microsoft.Extensions.Configuration;

namespace ECS.MemberManager.Core.DataAccess
{
    public static class DalFactory
    {
        private static Type _dalType;
        private static readonly IConfigurationRoot _configurationRoot;

        static DalFactory()
        {
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.json", optional: true, true);
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
                    throw new ArgumentException($"Type {dalTypeName} could not be found");
            }

            return (IDalManager) Activator.CreateInstance(_dalType);
        }
    }
}