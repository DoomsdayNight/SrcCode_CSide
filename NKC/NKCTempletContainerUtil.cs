using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using NKM.Templet.Base;

namespace NKC
{
	// Token: 0x020006D6 RID: 1750
	internal static class NKCTempletContainerUtil
	{
		// Token: 0x06003D35 RID: 15669 RVA: 0x0013ABE0 File Offset: 0x00138DE0
		static NKCTempletContainerUtil()
		{
			Type openedContainerType = typeof(NKMTempletContainer<>);
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			NKCTempletContainerUtil.containerTypes = (from templetType in executingAssembly.GetTypes().Where(new Func<Type, bool>(NKCTempletContainerUtil.<.cctor>g__Filter|2_2))
			select openedContainerType.MakeGenericType(new Type[]
			{
				templetType
			})).ToArray<Type>();
			NKCTempletContainerUtil.containerTypesEx = (from templetType in executingAssembly.GetTypes().Where(new Func<Type, bool>(NKCTempletContainerUtil.<.cctor>g__FilterEx|2_3))
			select openedContainerType.MakeGenericType(new Type[]
			{
				templetType
			})).ToArray<Type>();
		}

		// Token: 0x06003D36 RID: 15670 RVA: 0x0013AC6C File Offset: 0x00138E6C
		public static void InvokeJoin()
		{
			Type[] array = NKCTempletContainerUtil.containerTypes;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].GetMethod("Join").Invoke(null, null);
			}
		}

		// Token: 0x06003D37 RID: 15671 RVA: 0x0013ACA4 File Offset: 0x00138EA4
		public static void InvokePostJoin()
		{
			Type[] array = NKCTempletContainerUtil.containerTypesEx;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].GetMethod("PostJoin").Invoke(null, null);
			}
		}

		// Token: 0x06003D38 RID: 15672 RVA: 0x0013ACDC File Offset: 0x00138EDC
		public static void InvokeValidate()
		{
			Type[] array = NKCTempletContainerUtil.containerTypes;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].GetMethod("Validate").Invoke(null, null);
			}
		}

		// Token: 0x06003D39 RID: 15673 RVA: 0x0013AD14 File Offset: 0x00138F14
		public static void InvokeDrop()
		{
			Type[] array = NKCTempletContainerUtil.containerTypes;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].GetMethod("Drop").Invoke(null, null);
			}
		}

		// Token: 0x06003D3A RID: 15674 RVA: 0x0013AD4A File Offset: 0x00138F4A
		[CompilerGenerated]
		internal static bool <.cctor>g__Filter|2_2(Type type)
		{
			return type.GetInterface("INKMTemplet") != null && type.GetCustomAttribute<SkipDerivedClassJoinAttribute>() == null;
		}

		// Token: 0x06003D3B RID: 15675 RVA: 0x0013AD6A File Offset: 0x00138F6A
		[CompilerGenerated]
		internal static bool <.cctor>g__FilterEx|2_3(Type type)
		{
			return type.GetInterface("INKMTempletEx") != null && type.GetCustomAttribute<SkipDerivedClassJoinAttribute>() == null;
		}

		// Token: 0x0400362F RID: 13871
		private static readonly Type[] containerTypes;

		// Token: 0x04003630 RID: 13872
		private static readonly Type[] containerTypesEx;
	}
}
