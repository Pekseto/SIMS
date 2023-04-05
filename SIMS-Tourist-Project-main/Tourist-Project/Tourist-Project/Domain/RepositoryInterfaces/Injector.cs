using System;
using System.Collections.Generic;

namespace Tourist_Project.Domain.RepositoryInterfaces
{
    public class Injector
    {
        private static Dictionary<Type, object> implementations = new Dictionary<Type, object>
        {
        };

        public static T CreateInstance<T>()
        {
            Type type = typeof(T);

            if (implementations.ContainsKey(type))
            {
                return (T)implementations[type];
            }

            throw new ArgumentException($"No implementation found for type {type}");
        }
    }
}