using System;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x02000341 RID: 833
	internal static class SetPropertyUtility
	{
		// Token: 0x06001398 RID: 5016 RVA: 0x0004950C File Offset: 0x0004770C
		public static bool SetColor(ref Color currentValue, Color newValue)
		{
			if (currentValue.r == newValue.r && currentValue.g == newValue.g && currentValue.b == newValue.b && currentValue.a == newValue.a)
			{
				return false;
			}
			currentValue = newValue;
			return true;
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x0004955B File Offset: 0x0004775B
		public static bool SetStruct<T>(ref T currentValue, T newValue) where T : struct
		{
			if (currentValue.Equals(newValue))
			{
				return false;
			}
			currentValue = newValue;
			return true;
		}

		// Token: 0x0600139A RID: 5018 RVA: 0x0004957C File Offset: 0x0004777C
		public static bool SetClass<T>(ref T currentValue, T newValue) where T : class
		{
			if ((currentValue == null && newValue == null) || (currentValue != null && currentValue.Equals(newValue)))
			{
				return false;
			}
			currentValue = newValue;
			return true;
		}
	}
}
