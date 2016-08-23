using Past.Protocol;
using Past.Utils;
using System;
using System.Linq;
using System.Reflection;

namespace Past.Network.Handlers
{
    public class MessageHandlerManager<C>
    {
        public static void InitializeHandlers()
        {
            var methods = 
                Assembly.GetExecutingAssembly().GetTypes()
                .SelectMany(t => t.GetMethods())
                .Where(m => m.Name.EndsWith("Handler") && m.GetParameters().Length == 2 && 
                (typeof(NetworkMessage).IsAssignableFrom(m.GetParameters().ElementAt(1).ParameterType) && 
                m.GetParameters().ElementAt(0).ParameterType == typeof(C)));

            foreach (var method in methods)
            {
                //todo: make check to packetType, client type..
                var packet_type = method.GetParameters()[1].ParameterType; //packet type!
                var t = typeof(Messages<>).MakeGenericType(typeof(C), packet_type);
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
                if (message != null) //?
                {
                    Console.WriteLine(typeof(T));
                    var h = Messages<T>.Handler;
                    if (h != null)
                        h(client, message);
                    else
                        ConsoleUtils.Write(ConsoleUtils.type.WARNING, "Received Unknown packet : {0} ...", message);
                }
                else
                {
                    ConsoleUtils.Write(ConsoleUtils.type.WARNING, "Receive empty packet ...");
                }
            }
            catch (Exception ex)
            {
                ConsoleUtils.Write(ConsoleUtils.type.ERROR, ex.ToString());
            }
        }
    }
}
