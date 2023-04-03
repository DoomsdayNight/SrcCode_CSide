using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace NLua.Extensions
{
	// Token: 0x0200007A RID: 122
	internal static class TypeExtensions
	{
		// Token: 0x06000444 RID: 1092 RVA: 0x00014990 File Offset: 0x00012B90
		public static bool HasMethod(this Type t, string name)
		{
			return t.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public).Any((MethodInfo m) => m.Name == name);
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x000149C3 File Offset: 0x00012BC3
		public static bool HasAdditionOperator(this Type t)
		{
			return t.IsPrimitive || t.HasMethod("op_Addition");
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x000149DA File Offset: 0x00012BDA
		public static bool HasSubtractionOperator(this Type t)
		{
			return t.IsPrimitive || t.HasMethod("op_Subtraction");
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x000149F1 File Offset: 0x00012BF1
		public static bool HasMultiplyOperator(this Type t)
		{
			return t.IsPrimitive || t.HasMethod("op_Multiply");
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x00014A08 File Offset: 0x00012C08
		public static bool HasDivisionOperator(this Type t)
		{
			return t.IsPrimitive || t.HasMethod("op_Division");
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x00014A1F File Offset: 0x00012C1F
		public static bool HasModulusOperator(this Type t)
		{
			return t.IsPrimitive || t.HasMethod("op_Modulus");
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00014A36 File Offset: 0x00012C36
		public static bool HasUnaryNegationOperator(this Type t)
		{
			return t.IsPrimitive || t.GetMethod("op_UnaryNegation", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public) != null;
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x00014A55 File Offset: 0x00012C55
		public static bool HasEqualityOperator(this Type t)
		{
			return t.IsPrimitive || t.HasMethod("op_Equality");
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x00014A6C File Offset: 0x00012C6C
		public static bool HasLessThanOperator(this Type t)
		{
			return t.IsPrimitive || t.HasMethod("op_LessThan");
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x00014A83 File Offset: 0x00012C83
		public static bool HasLessThanOrEqualOperator(this Type t)
		{
			return t.IsPrimitive || t.HasMethod("op_LessThanOrEqual");
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x00014A9C File Offset: 0x00012C9C
		public static MethodInfo[] GetMethods(this Type t, string name, BindingFlags flags)
		{
			return (from m in t.GetMethods(flags)
			where m.Name == name
			select m).ToArray<MethodInfo>();
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x00014AD4 File Offset: 0x00012CD4
		public static MethodInfo[] GetExtensionMethods(this Type type, string name, IEnumerable<Assembly> assemblies = null)
		{
			List<Type> list = new List<Type>();
			list.AddRange(from t in type.Assembly.GetTypes()
			where t.IsPublic
			select t);
			if (assemblies != null)
			{
				foreach (Assembly assembly in assemblies)
				{
					if (!(assembly == type.Assembly))
					{
						list.AddRange(from t in assembly.GetTypes()
						where t.IsPublic && t.IsClass && t.IsSealed && t.IsAbstract && !t.IsNested
						select t);
					}
				}
			}
			return (from extensionType in list
			from method in extensionType.GetMethods(name, BindingFlags.Static | BindingFlags.Public)
			select new
			{
				extensionType,
				method
			} into t
			where t.method.IsDefined(typeof(ExtensionAttribute), false)
			where t.method.GetParameters()[0].ParameterType == type || t.method.GetParameters()[0].ParameterType.IsAssignableFrom(type) || type.GetInterfaces().Contains(t.method.GetParameters()[0].ParameterType)
			select t.method).ToArray<MethodInfo>();
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x00014C40 File Offset: 0x00012E40
		public static MethodInfo GetExtensionMethod(this Type t, string name, IEnumerable<Assembly> assemblies = null)
		{
			MethodInfo[] array = t.GetExtensionMethods(name, assemblies).ToArray<MethodInfo>();
			if (array.Length == 0)
			{
				return null;
			}
			return array[0];
		}
	}
}
