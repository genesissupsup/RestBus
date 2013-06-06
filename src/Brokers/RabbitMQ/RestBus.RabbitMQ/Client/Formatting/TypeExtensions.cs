//Sourced from https://github.com/mono/aspnetwebstack/blob/master/src/System.Net.Http.Formatting/Internal/TypeExtensions.cs
// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

namespace RestBus.RabbitMQ.Client.Formatting
{
    using System;
    using System.Linq;
    using System.Reflection;

    internal static class TypeExtensions
    {
#if NETFX_CORE
    private static bool EqualTo(this Type[] t1, Type[] t2)
    {
        if (t1.Length != t2.Length)
        {
            return false;
        }

        for (int idx = 0; idx < t1.Length; ++idx)
        {
            if (t1[idx] != t2[idx])
            {
                return false;
            }
        }

        return true;
    }

    public static ConstructorInfo GetConstructor(this Type type, Type[] types)
    {
        return type.GetTypeInfo().DeclaredConstructors
                                    .Where(c => c.IsPublic)
                                    .SingleOrDefault(c => c.GetParameters()
                                                        .Select(p => p.ParameterType).ToArray().EqualTo(types));
    }
#endif

        public static Type ExtractGenericInterface(this Type queryType, Type interfaceType)
        {
            Func<Type, bool> matchesInterface = t => t.IsGenericType() && t.GetGenericTypeDefinition() == interfaceType;
            return (matchesInterface(queryType)) ? queryType : queryType.GetInterfaces().FirstOrDefault(matchesInterface);
        }

#if NETFX_CORE
    public static Type[] GetGenericArguments(this Type type)
    {
        return type.GetTypeInfo().GenericTypeArguments;
    }

    public static Type[] GetInterfaces(this Type type)
    {
        return type.GetTypeInfo().ImplementedInterfaces.ToArray();
    }
#endif

#if NETFX_CORE
    public static bool IsAssignableFrom(this Type type, Type c)
    {
        return type.GetTypeInfo().IsAssignableFrom(c.GetTypeInfo());
    }
#endif

        public static bool IsGenericType(this Type type)
        {
#if NETFX_CORE
        return type.GetTypeInfo().IsGenericType;
#else
            return type.IsGenericType;
#endif
        }

        public static bool IsInterface(this Type type)
        {
#if NETFX_CORE
        return type.GetTypeInfo().IsInterface;
#else
            return type.IsInterface;
#endif
        }

        public static bool IsValueType(this Type type)
        {
#if NETFX_CORE
        return type.GetTypeInfo().IsValueType;
#else
            return type.IsValueType;
#endif
        }
    }
}
