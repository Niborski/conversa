// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;

namespace Conversa.Services
{
    public sealed class ServiceContainer
    {
        private static Dictionary<Type, object> _services = new Dictionary<Type, object>();

        static ServiceContainer()
        {
            // ServiceContainer.Register<AccountService>();
            // ServiceContainer.Register<NetworkAvailableService();
        }

        public static T Register<T>()
            where T : class, new()
        {
            var instance = Resolve<T>();

            if (instance == null)
            {
                _services.Add(typeof(T), new T());
            }

            return Resolve<T>();
        }

        public static T Resolve<T>()
            where T : class
        {
            var serviceType = typeof(T);

            if (_services.ContainsKey(serviceType))
            {
                return (T)_services[serviceType];
            }

            return null;
        }
    }
}
