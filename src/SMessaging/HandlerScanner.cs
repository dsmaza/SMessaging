using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SMessaging.Abstractions;

namespace SMessaging
{
    public class HandlerScanner : IEnumerable<KeyValuePair<Type, Type>>
    {
        private readonly Dictionary<Type, Type> messageHandlerMappings = new Dictionary<Type, Type>();

        public HandlerScanner ScanAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes();
            var handlers = FindHandlers(types, typeof(IHandleMessage<>));
            foreach (var item in handlers)
            {
                messageHandlerMappings.Add(item.Key, item.Value);
            }
            return this;
        }

        private static IEnumerable<KeyValuePair<Type, Type>> FindHandlers(Type[] types, Type genericType)
        {
            return from t in types
                   from i in t.GetInterfaces()
                   where i.IsConstructedGenericType
                   where i.GetGenericTypeDefinition() == genericType
                   let args = i.GetGenericArguments()
                   select new KeyValuePair<Type, Type>(args[0], t);
        }

        public IEnumerator<KeyValuePair<Type, Type>> GetEnumerator()
        {
            return messageHandlerMappings.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
