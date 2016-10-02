using Past.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Past.Common.Utils
{
    public class MessageHandlerManager<C>
    {
        public static void InitializeHandlers()
        {
            IEnumerable<MethodInfo> methods = Assembly.GetExecutingAssembly().GetTypes().SelectMany(t => t.GetMethods()).Where(m => m.Name.StartsWith("Handle") && m.GetParameters().Length == 2 && (typeof(NetworkMessage).IsAssignableFrom(m.GetParameters().ElementAt(1).ParameterType) && m.GetParameters().ElementAt(0).ParameterType == typeof(C)));
            foreach (var method in methods)
            {
                Type packet_type = method.GetParameters()[1].ParameterType;
                Type t = typeof(Messages<>).MakeGenericType(typeof(C), packet_type);
                t.GetMethod("Register").Invoke(null, new object[] { method });
            }
        }

        public static class Messages<T> where T : NetworkMessage
        {
            public static Action<C, T> Handler { get; private set; }

            public static void Register(MethodInfo mi)
            {
                Handler = (Action<C, T>)mi.CreateDelegate(typeof(Action<C, T>));
            }
        }

        public static void InvokeHandler<T>(C client, T message) where T : NetworkMessage
        {
            try
            {
                if (message != null)
                {
                    Action<C, T> h = Messages<T>.Handler;
                    if (h != null)
                    {
                        h(client, message);
                    }
                    else
                    {
                        ConsoleUtils.Write(ConsoleUtils.Type.WARNING, "Received unknown packet : {0} ...", message);
                    }
                }
                else
                {
                    ConsoleUtils.Write(ConsoleUtils.Type.WARNING, "Received empty packet : {0} ...", message);
                }
            }
            catch (Exception ex)
            {
                ConsoleUtils.Write(ConsoleUtils.Type.ERROR, ex.ToString());
            }
        }
    }
}
