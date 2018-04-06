﻿using System;
using System.Reflection;

namespace Meziantou.Framework.Utilities
{
    internal static class ReflectionUtilities
    {
        public static bool IsNullableOfT(this Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        public static bool IsFlagsEnum(Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            if (!type.IsEnum)
                return false;

            return type.IsDefined(typeof(FlagsAttribute), true);
        }

        public static MethodInfo GetImplicitConversion(object value, Type targetType)
        {
            if (value == null)
                return null;

            var valueType = value.GetType();
            return Array.Find(valueType.GetMethods(BindingFlags.Public | BindingFlags.Static), IsImplicitOperator);

            bool IsImplicitOperator(MethodInfo mi)
            {
                if (mi.Name != "op_Implicit")
                    return false;

                if (!targetType.IsAssignableFrom(mi.ReturnType))
                    return false;

                var p = mi.GetParameters();
                if (p.Length != 1)
                    return false;

                if (!p[0].ParameterType.IsAssignableFrom(valueType))
                    return false;

                return true;
            }
        }
    }
}
