using Past.Protocol;
using Past.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Past.Network.Handlers
{
    public class MessageHandlerManager
    {
        //Fallen
        public static Dictionary<uint, MethodInfo> MethodHandlers = new Dictionary<uint, MethodInfo>();

        public static void InitializeHandlers()
        {
            var methods = Assembly.GetExecutingAssembly().GetTypes()
                .SelectMany(t => t.GetMethods())
                .Where(m => m.GetCustomAttributes(typeof(MessageHandlerAttribute), false).Length > 0)
                .ToArray();
            foreach (var method in methods)
            {
                MethodHandlers.Add((uint)method.CustomAttributes.ToArray()[0].ConstructorArguments[0].Value, method);
            }
        }

        public static void InvokeHandler(object client, NetworkMessage message)
        {
            try
            {
                MethodInfo methodToInvok;
                if (message != null)
                {
                    if (MethodHandlers.TryGetValue(message.Id, out methodToInvok))
                    {
                        methodToInvok.Invoke(null, new object[] { client, message });
                    }
                    else
                    {
                        ConsoleUtils.Write(ConsoleUtils.type.WARNING, "Received Unknown packet : {0} ...", message);
                    }
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
