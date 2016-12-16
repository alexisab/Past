﻿using Past.Protocol.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;

namespace Past.Protocol
{
    public static class MessageReceiver
    {
        private static readonly Dictionary<uint, Type> Messages = new Dictionary<uint, Type>();
        private static readonly Dictionary<uint, Func<NetworkMessage>> Constructors = new Dictionary<uint, Func<NetworkMessage>>();

        [Serializable]
        public class MessageNotFoundException : Exception
        {
            public MessageNotFoundException()
            {
            }
            public MessageNotFoundException(string message) : base(message)
            {
            }
            public MessageNotFoundException(string message, Exception inner) : base(message, inner)
            {
            }
            protected MessageNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }

        public static void InitializeMessages()
        {
            var assembly = Assembly.GetAssembly(typeof(MessageReceiver));
            foreach (var type in assembly.GetTypes().Where(entry => entry.IsSubclassOf(typeof(NetworkMessage))))
            {
                var property = type.GetProperty("Id");
                if (property != null)
                {
                    uint num = (uint)property.GetValue(Activator.CreateInstance(type), null);
                    if (MessageReceiver.Messages.ContainsKey(num))
                    {
                        throw new AmbiguousMatchException(
                            $"MessageReceiver() => {num} item is already in the dictionary, old type is : {MessageReceiver.Messages[num]}, new type is  {type}");
                    }
                    MessageReceiver.Messages.Add(num, type);
                    ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);
                    if (constructor == null)
                    {
                        throw new Exception($"'{type}' doesn't implemented a parameterless constructor");
                    }
                    MessageReceiver.Constructors.Add(num, constructor.CreateDelegate<Func<NetworkMessage>>());
                }
            }
        }

        public static NetworkMessage BuildMessage(uint id, BigEndianReader reader)
        {
            if (!MessageReceiver.Messages.ContainsKey(id))
            {
                throw new MessageReceiver.MessageNotFoundException($"Message <id:{id}> doesn't exist");
            }
            NetworkMessage message = MessageReceiver.Constructors[id]();
            if (message == null)
            {
                throw new MessageReceiver.MessageNotFoundException(
                    $"Constructors[{id}] (delegate {MessageReceiver.Messages[id]}) does not exist");
            }
            message.Unpack(reader);
            return message;
        }

        public static Type GetMessageType(uint id)
        {
            if (!MessageReceiver.Messages.ContainsKey(id))
            {
                throw new MessageReceiver.MessageNotFoundException($"Message <id:{id}> doesn't exist");
            }
            return MessageReceiver.Messages[id];
        }

        #region extension
        public static T CreateDelegate<T>(this ConstructorInfo ctor)
        {
            List<ParameterExpression> list = (
                from param in ctor.GetParameters()
                select Expression.Parameter(param.ParameterType)).ToList<ParameterExpression>();
            Expression<T> expression = Expression.Lambda<T>(Expression.New(ctor, list), list);
            return expression.Compile();
        }
        #endregion
    }
}